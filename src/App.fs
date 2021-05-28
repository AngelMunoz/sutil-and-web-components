module App

open Fable.Core
open Sutil
open Sutil.DOM
open Sutil.Attr
open Types
open Pages
open Components.Skeleton

type private State = { page: Page }

type private Msg = NavigateTo of Page

let private init () : State * Cmd<Msg> = { page = Home }, Cmd.none

let private update (msg: Msg) (state: State) : State * Cmd<Msg> =
  match msg with
  | NavigateTo page -> { state with page = page }, Cmd.none

let private navigateTo (dispatch: Dispatch<Msg>) (page: Page) =
  NavigateTo page |> dispatch



let private appContent (state) =
  bindFragment state
  <| fun state ->
       match state.page with
       | Home -> Home.view ()
       | About ->
           Html.article [
             Html.h1 [
               text "Sutil is an F# abstraction over Svelte"
             ]
             Html.p [
               text
                 """
                  Svelte is a powerful frontend framework which embraces reactivity,
                  Sutil takes advantage of the Fable compiler to bring Svelte to F# developer's toolbox
                  """
             ]
           ]


let view () =
  let state, dispatch = Store.makeElmish init update ignore ()
  let navigateTo = navigateTo dispatch

  Html.app [
    disposeOnUnmount [ state ]
    class' "app-content"
    Html.nav [
      class' "app-nav"
      Html.custom (
        "sl-button",
        [ onClick (fun _ -> navigateTo Home) []
          type' "text"
          text "Home" ]
      )
      Html.custom (
        "sl-button",
        [ onClick (fun _ -> navigateTo About) []
          type' "text"
          text "About" ]
      )
    ]
    Html.main [
      bindPromise
        (Async.Sleep(1500) |> Async.StartAsPromise)
        (skeleton ())
        (fun _ -> appContent (state))
        (fun ex -> text $"{ex.Message}")
    ]
  ]
