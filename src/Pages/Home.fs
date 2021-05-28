module Pages.Home

open Browser.Dom
open Fable.Core
open Sutil
open Sutil.DOM
open Sutil.Attr
open Components
open Components.Alert
open Sutil.Styling
open Browser.Types

let componentsSection () =
  Html.section [
    class' "row-spaced"
    Counter.view (None)
    Counter.view (Some 42)
  ]

let eventsSection () =

  let printValue (e: Event) =
    // current work around untill a new release with `onCustomEvent<'T>` is out
    let event =
      (e :?> CustomEvent<{| item: {| value: string |} |}>)

    match event.detail with
    | Some event -> printfn $"Got: {event.item.value}"
    | None -> printfn "Got nothing"

  Html.section [
    class' "row-spaced"
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
    Html.custom (
      "sl-menu",
      [ Html.custom ("sl-menu-item", [ Attr.value "First"; text "First" ])
        Html.custom ("sl-menu-item", [ Attr.value "Second"; text "Second" ])
        Html.custom ("sl-menu-divider", [])
        on "sl-select" printValue [] ]
    )
  ]

type SlDialog =
  inherit HTMLElement

  abstract member show : unit -> JS.Promise<unit>
  abstract member hide : unit -> JS.Promise<unit>

let dialogsSection () =
  let alertStore : IStore<SlAlertProps> =
    Store.make
      { closable = true
        duration = Some 3500.
        open' = false
        type' = None }

  let openDialog (e: Event) =
    let dialog = document.querySelector ("sl-dialog")
    (dialog :?> SlDialog).show () |> ignore

  let closeDialog (e: Event) =
    let e = (e.target :?> HTMLElement)
    (e.parentElement :?> SlDialog).hide () |> ignore

  Html.section [
    Html.section [
      disposeOnUnmount [ alertStore ]
      class' "row-spaced"
      Html.custom (
        "sl-button",
        [ text "Open Alert"
          type' "info"
          onClick
            (fun _ ->
              alertStore
              |> Store.modify (fun store -> { store with open' = true }))
            [] ]
      )
      Html.custom (
        "sl-button",
        [ type' "primary"
          text "Open Dialog"
          onClick openDialog [] ]
      )
    ]
    Alert(
      alertStore,
      [ Html.p [
          text
            "This is a sample on how you can make components with from existing libraries that may fit better in your applications"
        ]
        Html.custom (
          "sl-button",
          [ text "Close"
            onClick
              (fun _ ->
                alertStore
                |> Store.modify (fun store -> { store with open' = false }))
              [] ]
        ) ]
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
  |> withStyle [
       rule "sl-button" [ Css.margin (Feliz.length.em 1) ]
       rule
         ".row-spaced"
         [ Css.displayFlex
           Css.custom ("justify-content", "space-evenly") ]
     ]

let view () =
  Html.article [
    class' "page"
    Html.section [
      Html.p [
        Attr.style "text-align: center"
        text "Check the console when you click these buttons"
      ]
      eventsSection ()
    ]
    dialogsSection ()
    componentsSection ()
  ]
  |> withStyle [
       rule "section" [ Css.margin (Feliz.length.em 1) ]
       rule
         ".row-spaced"
         [ Css.displayFlex
           Css.custom ("justify-content", "space-evenly") ]
     ]
