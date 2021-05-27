module Pages.Home

open Sutil
open Sutil.DOM
open Sutil.Attr
open Components
open Types
open Sutil.Styling

let view () =

  Html.article [
    class' "page"
    Html.header [
      Html.p [
        text "Here we have two counter components with different params"
      ]
    ]
    Html.section [ Counter.view (None) ]
    Html.section [ Counter.view (Some 42) ]
  ]
  |> withStyle [
       rule "section" [ Css.margin (Feliz.length.em 1) ]
     ]
