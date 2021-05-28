module Pages.Home

open Browser.Dom
open Fable.Core
open Sutil
open Sutil.DOM
open Sutil.Attr
open Components
open Sutil.Styling
open Browser.Types

type SlDialog =
  inherit HTMLElement

  abstract member show : unit -> JS.Promise<unit>
  abstract member hide : unit -> JS.Promise<unit>

let view () =
  let isDialogOpen = Store.make false

  Html.article [
    disposeOnUnmount [ isDialogOpen ]
    class' "page"
    Html.header [
      Html.p [
        text "Here we have two counter components with different params"
      ]
    ]

    Html.section [
      Html.custom (
        "sl-button",
        [ type' "success"
          text "This is a Web Component Button"
          onClick (fun _ -> printfn "Hey success!") [] ]
      )
      Html.custom (
        "sl-button",
        [ Attr.custom ("circle", "")
          Attr.custom ("size", "large")
          onClick (fun _ -> printfn "Hey circle!") []
          Html.custom ("sl-icon", [ Attr.custom ("name", "gear") ]) ]
      )
    ]
    Html.section [
      let printValue (e: Event) =
        // current work around untill a new release with `onCustomEvent<'T>` is out
        let event =
          (e :?> CustomEvent<{| item: {| value: string |} |}>)

        match event.detail with
        | Some event -> printfn $"Got: {event.item.value}"
        | None -> printfn "Got nothing"

      Html.custom (
        "sl-menu",
        [ Html.custom ("sl-menu-item", [ Attr.value "First"; text "First" ])
          Html.custom ("sl-menu-item", [ Attr.value "Second"; text "Second" ])
          Html.custom ("sl-menu-divider", [])
          on "sl-select" printValue [] ]
      )
    ]
    Html.section [
      let openDialog (e: Event) =
        let dialog = document.querySelector ("sl-dialog")
        (dialog :?> SlDialog).show () |> ignore

      let closeDialog (e: Event) =
        let e = (e.target :?> HTMLElement)
        (e.parentElement :?> SlDialog).hide () |> ignore

      Html.custom (
        "sl-button",
        [ type' "warning"
          text "Open Dialog"
          onClick openDialog [] ]
      )

      Html.custom (
        "sl-dialog",
        [ Attr.custom ("label", "My Dialog")
          text "Yaaay Insideeee"
          Html.custom (
            "sl-button",
            [ Attr.custom ("slot", "footer")
              type' "primary"
              text "Close"
              onClick closeDialog [] ]
          ) ]
      )
    ]
    Html.section [ Counter.view (None) ]
    Html.section [ Counter.view (Some 42) ]
  ]
  |> withStyle [
       rule "section" [ Css.margin (Feliz.length.em 1) ]
     ]
