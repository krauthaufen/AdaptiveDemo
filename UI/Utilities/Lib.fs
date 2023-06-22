namespace AdaptiveDemo

open System
open Aardvark.Base
open Aardvark.Dom
open Aardvark.Rendering
open Aardworx.WebAssembly
open Aardworx.WebAssembly.Dom
open FSharp.Data.Adaptive

module UI =
    let run gl (ui : Env<unit> -> DomNode) =
        let app =
            {
                initial = ()
                update = fun _ () () -> ()
                view = fun e () -> ui e
                unpersist =
                    {
                        init = fun () -> ()
                        update = fun () () -> ()
                    }
            }
        Boot.run gl app

[<AutoOpen>]
module RenderingHelpers =
        
    type RenderBuilder() =
        inherit RenderControlBuilder()
        
        member x.Run(run : NodeBuilderHelpers.RenderControlBuilder<unit>) =
            let getContent (info : RenderControlInfo) =
                
                let time =
                    let t0 = DateTime.Now
                    let sw = System.Diagnostics.Stopwatch.StartNew()
                    AVal.custom (fun _ ->
                        t0 + sw.Elapsed
                    )
                let state = NodeBuilderHelpers.RenderControlBuilderState( { info with Time = time } )
                
                let mutable camera = OrbitState.create V3d.Zero 1.0 0.4 7.0 Button.Left Button.Right
                let cam = cval camera.view
                let coll = new AsyncCollection<seq<OrbitMessage>>()
                let orbitEnv =
                    { new Env<OrbitMessage>  with
                        member x.Emit messages =
                            coll.Put messages
                        member x.Run(js, cb) =
                            printfn "cannot run JS"
                        member x.RunModal _ =
                            printfn "cannot run modal"
                            { new IDisposable with member x.Dispose() = () }
                        member x.Runtime =
                            info.Runtime
                    }
                  
                let runner =
                    task {
                        while true do
                            let! msgs = coll.Take()
                            for msg in msgs do
                                camera <- OrbitController.update orbitEnv camera msg
                                
                                match msg with
                                | OrbitMessage.Rendered -> 
                                    transact (fun () -> time.MarkOutdated())
                                | _ ->
                                    ()
                            //camera <- OrbitState.withView camera
                            transact (fun () -> cam.Value <- camera.view)
                    }
                    
                let camera, attributes =
                    let att = OrbitController.getAttributes orbitEnv
                    cam, att
                    
                
                let a, b =
                    RenderControl.OnRendered (fun _ -> 
                        orbitEnv.Emit [OrbitMessage.Rendered]
                    )
                state.Append (a, b)
                
                state.Append attributes
                state.Append [
                    Sg.View (
                        camera |> AVal.map (fun v ->
                            CameraView.viewTrafo v
                        )
                    )
                    
                    Sg.Proj (
                        info.ViewportSize |> AVal.map (fun s ->
                            Frustum.perspective 50.0 0.1 100.0 (float s.X / float s.Y)
                            |> Frustum.projTrafo
                        ) 
                    )
                    
                    Sg.OnDoubleTap (fun e ->
                        orbitEnv.Emit [ OrbitMessage.SetTargetCenter(true, AnimationKind.Tanh, e.WorldPosition) ]    
                    )
                    
                ]
                run state
                state.Build()
            DomNode.RenderControl(getContent)
        
    let simpleRenderControl = RenderBuilder()
      
