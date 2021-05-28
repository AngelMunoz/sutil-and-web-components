module Components.Skeleton

open Sutil
open Sutil.DOM
open Sutil.Attr
open Sutil.Styling

let skeleton () =
  let random = System.Random()

  Html.article [
    class' "skeleton"
    Html.header [
      for i in 0 .. random.Next(5) do
        Html.custom ("sl-skeleton", [])
    ]
    for i in 0 .. random.Next(10) do
      Html.custom ("sl-skeleton", [])
    Html.footer [
      for i in 0 .. random.Next(4) do
        Html.custom ("sl-skeleton", [])
    ]
  ]
  |> withStyle [
       rule ".skeleton" [ Css.marginTop (Feliz.length.em 2) ]
       rule "sl-skeleton" [ Css.margin (Feliz.length.em 1.5) ]
       rule
         "header"
         [ Css.marginBottom (Feliz.length.em 3)
           Css.borderStyleGroove
           Css.borderWidth 2
           Css.borderRadius 15 ]
       rule
         "footer"
         [ Css.borderStyleGroove
           Css.borderWidth 2
           Css.borderRadius 15 ]
       rule
         "sl-skeleton:nth-child(even)"
         [ Css.width (Feliz.length.percent (random.Next(25, 50))) ]
       rule
         "sl-skeleton:nth-child(odd)"
         [ Css.width (Feliz.length.percent (random.Next(0, 78))) ]
     ]
