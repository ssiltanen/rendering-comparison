﻿open Microsoft.AspNetCore.Builder
open Giraffe
open Giraffe.ViewEngine

[<Struct>]
type Player = { name: string; score: uint }

type Players = { players: Player[] }

let players =
    "../players.json"
    |> System.IO.File.OpenRead
    |> System.Text.Json.JsonSerializer.Deserialize<Players>
    |> _.players
    |> Array.toList

let index content =
    html
        [ _lang "en" ]
        [ head
              []
              [ meta [ _charset "UTF-8" ]
                meta [ _httpEquiv "X-UA-Compatible"; _content "IE=edge" ]
                meta [ _name "viewport"; _content "width=device-width, initial-scale=1.0" ]
                link [ _rel "stylesheet"; _href "/styles.css" ]
                title [] [ encodedText "F# Template Rendering" ] ]
          body [ _class "container" ] content ]

let playerDiv player =
    li [ _class "my-2 ml-2 bg-white odd:bg-gray-50" ] [ encodedText $"{player.name} - {player.score}" ]

let playerList players =
    [ div
          [ _class "ml-2" ]
          [ h1 [ _class "text-xl my-4" ] [ encodedText "Players" ]
            ul [ _class "border" ] (players |> List.map playerDiv) ] ]

let routes = choose [ route "/" >=> htmlView (players |> playerList |> index) ]

let builder = WebApplication.CreateBuilder()
builder.Services.AddGiraffe() |> ignore

let app = builder.Build()
app.UseGiraffe(routes)
app.UseStaticFiles() |> ignore
app.Run "http://localhost:8000"
