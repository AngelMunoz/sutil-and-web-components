module App

open Sutil
open Sutil.DOM
open Sutil.Attr
open Sutil.Styling
open Types
open Pages

type State = { page: Page }

type Msg = NavigateTo of Page

let init () : State * Cmd<Msg> = { page = Home }, Cmd.none

let update (msg: Msg) (state: State) : State * Cmd<Msg> =
  match msg with
  | NavigateTo page -> { state with page = page }, Cmd.none

let navigateTo (dispatch: Dispatch<Msg>) (page: Page) =
  NavigateTo page |> dispatch

let view () =
  let state, dispatch = Store.makeElmish init update ignore ()
  let navigateTo = navigateTo dispatch

  Html.app [
    disposeOnUnmount [ state ]
    class' "app-content"
    Html.nav [
      class' "app-nav"
      Html.a [
        class' "app-nav-link"
        onClick (fun _ -> navigateTo Home) []
        text "Home"
      ]
      Html.a [
        class' "app-nav-link"
        onClick (fun _ -> navigateTo About) []
        text "About"
      ]
    ]
    Html.main [
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
    ]
  ]
