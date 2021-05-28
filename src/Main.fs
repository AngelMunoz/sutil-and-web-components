module Main

open Fable.Core
open Sutil.DOM
open Fable.Core.JsInterop

importSideEffects "@shoelace-style/shoelace/dist/themes/base.css"
importSideEffects "./styles.css"

importDefault "@shoelace-style/shoelace/dist/components/button/button.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/alert/alert.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/card/card.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/skeleton/skeleton.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/icon/icon.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/menu/menu.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/menu-item/menu-item.js"
|> ignore

importDefault
  "@shoelace-style/shoelace/dist/components/menu-divider/menu-divider.js"
|> ignore

importDefault "@shoelace-style/shoelace/dist/components/dialog/dialog.js"
|> ignore

importDefault
  "@shoelace-style/shoelace/dist/components/icon-button/icon-button.js"
|> ignore

[<ImportMember("@shoelace-style/shoelace/dist/utilities/base-path.js")>]
let setBasePath (path: string) : unit = jsNative

setBasePath "shoelace"
// Start the app
App.view () |> mountElement "sutil-app"
