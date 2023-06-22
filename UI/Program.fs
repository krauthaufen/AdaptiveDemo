open System
open Aardvark.Base
open Aardvark.Dom
open Aardvark.Rendering
open Aardworx.WebAssembly
open FSharp.Data.Adaptive
open Aardworx.Rendering.WebGL
open AdaptiveDemo

  
let ui (_env : Env<unit>) =
    let state = cval 0
    
    div {
        h1 {
            state |> AVal.map (fun v ->
                if v < 0 then Some (Style [Color "red"])
                else None
            )
            AVal.map (sprintf "State: %d") state
        }
        button {
            Dom.OnClick (fun _ ->
                transact (fun () -> state.Value <- state.Value + 1)
            )
            "+"
        }
        button {
            Dom.OnClick (fun _ ->
                transact (fun () -> state.Value <- state.Value - 1)
            )
            "-"
        }
        
        simpleRenderControl {
            Style [
                ZIndex -1
                Position "fixed"
                Top "0px"; Left "0px"
                Width "100%"; Height "100%"
                Background "linear-gradient(to top, #051937, #314264, #5d6f95, #8ba1c9, #bbd5ff)"
            ]
            let! info = RenderControl.Info
            
            sg {
                Sg.Trafo(Trafo3d.Scale(10.0, 10.0, 1.0))
            
                Sg.Shader {
                    DefaultSurfaces.trafo
                    DefaultSurfaces.diffuseTexture
                }
                
                Sg.Uniform("DiffuseColorTexture", DefaultTextures.checkerboard)
                Primitives.ScreenQuad -0.5
                
            }
            
            sg {
                
                Sg.Translate (
                    state |> AVal.map (fun i ->
                        V3d(0.0, 0.0, float i * 0.25)
                    )
                )
                
                Sg.Trafo (
                    let t0 = DateTime.Now
                    info.Time |> AVal.map (fun t ->
                        Trafo3d.RotationZ(1.5 * (t - t0).TotalSeconds)
                    )
                )
                
                sg {
                    Sg.Shader {
                        DefaultSurfaces.trafo
                        DefaultSurfaces.simpleLighting
                    }
                    
                    Sg.Cursor "pointer"
                    Sg.OnClick (fun _ ->
                        transact (fun () ->
                            state.Value <- 0    
                        )    
                    )
                    
                    Primitives.Box(V3d.III, color = C4b.Blue)
                }
                
                sg {
                    let color =
                        state |> AVal.map (fun v ->
                            if v >= 0 then C4b.Black
                            else C4b.Red
                        )
                    Sg.Trafo (Trafo3d.RotationX Constant.PiHalf)
                    Sg.Scale 0.5
                    Sg.Translate(0.0, 0.0, 1.0)
                    Sg.Text(
                        text = AVal.map (sprintf "State: %A") state,
                        color = color,
                        align = TextAlignment.Center
                    )
                }
            }
        }
        
    }
   


[<EntryPoint>]
let main _args =
    task {
        do! Window.Document.Ready
        let app = new WebGLApplication(CommandStreamMode.Managed)
        
        UI.run app (fun env ->
            body { 
                Style [
                    Position "fixed"
                    Overflow "hidden"
                    Width "100%"; Height "100%"
                    Margin "0"; Padding "0"; Border "none"
                    StyleProperty("overscroll", "none")
                ]
                
                OnBoot [
                    "{ let e = document.getElementById('loader'); if(e) { e.remove(); } }"
                ]
                ui env
            }
        )
    } |> ignore
    0

