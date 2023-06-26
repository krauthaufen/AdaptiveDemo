namespace AdaptiveDemo

// In this code we illustrate why IObservable<'a> cannot easily be used for expressing
// changeable values with dynamic dependencies. Since IObservable focuses on a different
// use-case this is not to be considered a shortcoming of the interface but just a conceptual
// difference between IObservable and aval.

open System
open FSharp.Data.Adaptive
    
    
/// Since Observable doesn't provide a `bind` operator we have to implement it ourselves.
/// Note that the conceptual problem with expressing "changeable values" from below cannot be solved
/// with this type-signature. The main problem is that IObservable doesn't remember any state and cannot
/// be forced/adapted to do so without major performance implications.
module Observable =
    let bind (mapping : 'a -> IObservable<'b>) (input : IObservable<'a>) =
        { new IObservable<'b> with
            member x.Subscribe(obs : IObserver<'b>) =
                let mutable inner = { new IDisposable with member x.Dispose() = () }
                let outer = 
                    input.Subscribe(fun a ->
                        let b = mapping a
                        inner <- b.Subscribe(obs)
                    )
                { new IDisposable with
                    member x.Dispose() =
                        inner.Dispose()
                        outer.Dispose()
                }
                
                
        }


module ObservableComparison =
 
    let observableCode() =
        
        printfn "observable"
        let a = Event<int>()
        let b = Event<int>()
        let takeA = Event<bool>()
        
        let res =
            takeA.Publish |> Observable.bind (fun v ->
                if v then a.Publish :> _
                else b.Publish :> _
            )
            
        
        printfn "  takeA = true, a = 1 -> res = 1 (as intended)"
        res.Add (printfn "    res: %A")
        takeA.Trigger true
        a.Trigger 1
        
        printfn "  b = 10 -> no reaction (as intended)"
        b.Trigger 10
        printfn "  b = 20 -> no reaction (as intended)"
        b.Trigger 20
        
        printfn "  takeA = false -> no reaction (not intended)"
        takeA.Trigger false

    let adaptiveCode() =
        printfn "AVal"
        let a = cval 1
        let b = cval 0
        let takeA = cval true
        
        let res =
            takeA |> AVal.bind (fun v ->
                if v then a :> _
                else b :> _
            )
        
        
        printfn "  takeA = true, a = 1 -> res = 1"
        res.AddCallback(printfn "    res: %A") |> ignore
        
        printfn "  b = 10 -> no reaction (as intended)"
        transact (fun () -> b.Value <- 10)
        printfn "  b = 20 -> no reaction (as intended)"
        transact (fun () -> b.Value <- 20)
        
        printfn "  takeA = false"
        transact (fun () -> takeA.Value <- false)
      
    let test() =
        observableCode()
        adaptiveCode()
        