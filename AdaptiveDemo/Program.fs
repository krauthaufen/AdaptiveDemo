open FSharp.Data.Adaptive

/// a very basic example summing two inputs
let map2Example() =
    // define out two inputs
    let input1 = cval 10
    let input2 = cval 10
    
    // sum both inputs and print when evaluating.
    let dependent : aval<int> =
        (input1, input2) ||> AVal.map2 (fun v1 v2 ->
            printfn "  evaluate with %d %d" v1 v2
            v1 + v2
        )
        
    // force the result and print it
    printfn "initial force (10 + 10)"
    printfn "  %d" (dependent.GetValue AdaptiveToken.Top) // 20

    // changing input1 will not trigger any evaluation here (laziness)
    transact (fun () ->
        input1.Value <- 2
    )
        
    // forcing the result again will trigger the evaluation
    printfn "force after change (2 + 10)"
    printfn "  %d" (AVal.force dependent) // 12

    // changing two inputs at once (or in two transactions) will also not
    // trigger evaluation.
    transact (fun () ->
        input1.Value <- 40
        input2.Value <- 2
    )

    // forcing the result again will trigger the evaluation once
    printfn "force after change (40 + 2)"
    printfn "  %d" (AVal.force dependent) // 42
    
/// a dynamic-dependency example
let bindExample() =
    
    // a very simple database of fruits and their amount.
    let apples = cval 10
    let oranges = cval 3
    let grapes = cval 100
    let data =
        Map.ofList [
            "apples", apples :> aval<_>
            "oranges", oranges :> aval<_>
            "grapes", grapes :> aval<_>
        ]
        
    // current fruit-of-interest
    let fruit = cval "apples"
    
    // the amount of the current fruit-of-interest
    let amount =
        // Note that the `AVal.bind` function doesn't require these values
        // to come from a finite table but for the sake of simplicity
        // we use a map here.
        fruit |> AVal.bind (fun name ->
            printfn $"  lookup \"{name}\""
            match Map.tryFind name data with
            | Some count ->
                // we could simply return count here but we want to
                // observer the evaluation here, therefore we map it
                // with an identity function.
                count |> AVal.map (fun c ->
                    printfn $"  calculate amount of \"{name}\": {c}"
                    c   
                )
            | None ->
                AVal.constant 0
        )
        
    // force the number of apples
    printfn $"initial evaluation"
    printfn $"  => {AVal.force amount} {fruit.Value}"
    
    // change the amount of oranges -> no effect
    printfn "change amount of oranges -> no effect"
    transact (fun () -> oranges.Value <- 4)
    printfn $"  => {AVal.force amount} {fruit.Value}"
    
    // switch to oranges
    printfn $"switch to oranges"
    transact (fun () -> fruit.Value <- "oranges")
    printfn $"  => {AVal.force amount} {fruit.Value}"
    
    // change the amount of oranges -> no effect
    printfn "change amount of oranges"
    transact (fun () -> oranges.Value <- 3)
    printfn $"  => {AVal.force amount} {fruit.Value}"
    
/// a list example showing why `aval<list<'a>>` is not the
/// best granularity for lists.
let listExample() =
    // try to create a changeable list via avals.
    let list = cval [1;2;3]
    
    // mapping on the list is very straight forward
    // but the mapping function will be called for each
    // element in the list whenever anything changes.
    let mapped : aval<list<int>> =
        list |> AVal.map (
            List.map (fun a ->
                printfn "  map %d" a
                a * 2
            )
        )
    
    // initial evaluation works as expected
    printfn "initial"
    printfn "  %A" (AVal.force mapped)
    
    // prepending an element to the list will trigger
    // recomputation of all list-elements.
    printfn "prepend 0"
    transact (fun () ->
        list.Value <- 0::list.Value
    )
    printfn "  %A" (AVal.force mapped)
    
    // clist / alist can improve this situation by
    // only recomputing the elements that are affected
    let list' = clist [1;2;3]
    let mapped' =
        list' |> AList.map (fun a ->
            printfn "  map %d" a
            a * 2
        )
    
    // initial evaluation is identical to the one above
    printfn "initial"
    printfn "  %A" (AList.force mapped' |> Seq.toList)
    
    // prepending an element to the list will only trigger
    // recomputation of the first element of the output-list.
    // Note that we use IndexList (instead of list) here, which is
    // also an immutable datastructure but enables better runtimes
    // for internally necessary operations.
    printfn "prepend 0"
    transact (fun () ->
        list'.Value <- IndexList.prepend 0 list'.Value
    )
    printfn "  %A" (AList.force mapped' |> Seq.toList)
 
    // AList also provides efficient fold-combinators.
    printfn "sum"
    let sum =
        mapped' |> AList.sumBy (fun v ->
            // print something to observe evaluation
            printfn "  sumBy %A" v
            v
        )
    printfn "  %d" (AVal.force sum)
    
    // adding to the list will only trigger one addition
    printfn "append 10"
    transact (fun () ->
        list'.Value <- IndexList.add 10 list'.Value
    )
    printfn "  %d" (AVal.force sum)
    
    // removing from the list will only subtract one element from the sum
    printfn "removeAt 1"
    transact (fun () ->
        list'.Value <- IndexList.removeAt 1 list'.Value
    )
    printfn "  %d" (AVal.force sum)
    
//map2Example()
//bindExample()
listExample()
