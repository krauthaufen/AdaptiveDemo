open FSharp.Data.Adaptive


let map2Example() =
    let input1 = cval 10
    let input2 = cval 10
    
    // sum both inputs and print when evaluating.
    let dependent : aval<int> =
        (input1, input2) ||> AVal.map2 (fun v1 v2 ->
            printfn "  evaluate with %d %d" v1 v2
            v1 + v2
        )
        
    printfn "initial force (10 + 10)"
    printfn "  %d" (dependent.GetValue AdaptiveToken.Top)

    transact (fun () ->
        input1.Value <- 2
    )
        
    printfn "force after change (2 + 10)"
    printfn "  %d" (AVal.force dependent)

    transact (fun () ->
        input1.Value <- 40
    )
    transact (fun () ->
        input2.Value <- 2
    )

    printfn "force after change (40 + 2)"
    printfn "  %d" (AVal.force dependent)
    
let bindExample() =
    let speedOfLight = 299792458.0
    let mass = cval 1.0
    let momentum = cval 1.0
    
    let velocity =
        mass |> AVal.bind (fun m ->
            printfn "  evaluate bind with %.3f" m
            if m <= 0.0 then
                // massless things travel at the speed of light
                AVal.constant speedOfLight
            else
                // p = m * v (not relativistic though)
                momentum |> AVal.map (fun p ->
                    printfn "  calculate v with p=%.3f, m=%.3f" p m
                    p / sqrt (m ** 2.0 + (p/speedOfLight) ** 2.0)
                )
        )
        
    printfn "initial force: mass = 1, momentum = 1"
    printfn "  %.3f" (AVal.force velocity)
        
    transact (fun () ->
        mass.Value <- 0.0    
        momentum.Value <- 5.0 
    )
    printfn "mass = 0, momentum = 5"
    printfn "  %.3f" (AVal.force velocity)
        
    transact (fun () ->
        momentum.Value <- 10.0    
    )
    printfn "momentum = 10"
    printfn "  %.3f" (AVal.force velocity)
        
    transact (fun () ->
        mass.Value <- 2.0    
    )
    printfn "mass = 2"
    printfn "  %.3f" (AVal.force velocity)
      
    transact (fun () ->
        momentum.Value <- 4.0    
    )
    printfn "momentum = 4"
    printfn "  %.3f" (AVal.force velocity)
 
let listExample() =
    let list = cval [1;2;3]
    let mapped : aval<list<int>> =
        list |> AVal.map (
            List.map (fun a ->
                printfn "  map %d" a
                a * 2
            )
        )
    
    printfn "initial"
    printfn "  %A" (AVal.force mapped)
    
    printfn "prepend 0"
    transact (fun () ->
        list.Value <- 0::list.Value
    )
    printfn "  %A" (AVal.force mapped)
    
    
    let list' = clist [1;2;3]
    let mapped' =
        list' |> AList.map (fun a ->
            printfn "  map %d" a
            a * 2
        )
    
    printfn "initial"
    printfn "  %A" (AList.force mapped' |> Seq.toList)
    
    printfn "prepend 0"
    transact (fun () ->
        list'.Value <- IndexList.prepend 0 list'.Value
    )
    printfn "  %A" (AList.force mapped' |> Seq.toList)
   
map2Example()
bindExample()
listExample()
