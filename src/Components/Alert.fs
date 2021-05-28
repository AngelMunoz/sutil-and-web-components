module Components.Alert

open Browser.Types
open Fable.Core
open Sutil.DOM
open Sutil.Attr
open Browser.Dom
open Sutil

type SlAlertProps =
  { closable: bool
    duration: float option
    open': bool
    type': string option }

let Alert (props: IStore<SlAlertProps>, content: NodeFactory seq) =
  let closable = props .> (fun props -> props.closable)

  let duration =
    props
    .> (fun props -> props.duration |> Option.defaultValue JS.Infinity)

  let open' = props .> (fun props -> props.open')

  let type' =
    props
    .> (fun props -> props.type' |> Option.defaultValue "info")

  Html.custom (
    "sl-alert",
    [ Bind.attr ("closable", closable)
      Bind.attr ("duration", duration)
      Bind.attr ("type", type')
      Bind.attr ("open", open')
      yield! content ]
  )
