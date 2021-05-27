[<RequireQualifiedAccess>]
module Components.Counter

open Sutil
open Sutil.Styling
open Sutil.DOM
open Sutil.Attr

let view (initial: int option) =
  let initial = initial |> Option.defaultValue 0
  // views are called once, if you want reactive data
  // you need to use observables to allow the rendering system react to these changes
  let store = Store.make (initial)

  Html.article [
    class' "counter"
    Html.header [
      text "A Simple Counter Component Example"
    ]
    Html.p [
      bindFragment store
      <| fun count -> text $"Count: {count}"
    ]
    Html.aside [
      // .> creates an observable from the result of the function
      // useful for value transformations/computations
      bindFragment (store .> fun num -> num * num)
      <| fun sqCount -> text $"Square Count: {sqCount}"
    ]
    Html.footer [
      Html.button [
        text "Increment"
        onClick
          (fun _ ->
            // |-> maps a function to obtain the value without the store wrapper
            (store |-> fun count -> count + 1)
            |> Store.set store)
          []
      ]
      Html.button [
        text "Reset"
        onClick (fun _ -> Store.set store initial) []
      ]
      Html.button [
        text "Decrement"
        onClick
          (fun _ ->
            // |-> maps a function to obtain the value without the store wrapper
            (store |-> fun count -> count - 1)
            |> Store.set store)
          []
      ]
    ]
  ]
  |> withStyle [
       rule
         "article.counter"
         [ Css.displayGrid
           Css.gap (Feliz.length.em 1)
           Css.gridTemplate (
             """
              'header header' auto
              'p aside' auto
              'footer footer' auto / 1fr 1fr
            """
           )
           Css.custom ("justify-items", "center") ]
       rule "header" [ Css.gridArea "header" ]
       rule "p" [ Css.gridArea "p" ]
       rule "aside" [ Css.gridArea "aside" ]
       rule
         "footer"
         [ Css.gridArea "footer"
           Css.displayFlex
           Css.custom ("justify-content", "space-evenly")
           Css.custom ("justify-self", "stretch") ]
     ]
