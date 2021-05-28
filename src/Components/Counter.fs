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

  Html.custom (
    "sl-card",
    [ class' "counter"
      Html.header [
        Attr.custom ("slot", "header")
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
        Html.custom (
          "sl-icon-button",
          [ Attr.name "plus"
            Attr.custom ("label", "Increment")
            onClick
              (fun _ ->
                // |-> maps a function to obtain the value without the store wrapper
                (store |-> fun count -> count + 1)
                |> Store.set store)
              [] ]
        )
        Html.custom (
          "sl-icon-button",
          [ Attr.name "arrow-counterclockwise"
            Attr.custom ("label", "Reset")
            onClick (fun _ -> Store.set store initial) [] ]
        )
        Html.custom (
          "sl-icon-button",
          [ Attr.name "dash"
            Attr.custom ("label", "Decrement")
            onClick
              (fun _ ->
                // |-> maps a function to obtain the value without the store wrapper
                (store |-> fun count -> count - 1)
                |> Store.set store)
              [] ]
        )
      ] ]
  )
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
       rule "sl-icon-button" [ Css.fontSize (Feliz.length.em 1.62) ]
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
