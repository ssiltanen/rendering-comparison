const fs = require("fs");

let players = [];

for (let i = 1; i <= 20000; i++) {
  let player = {
    name: `Player ${i}`,
    score: 0,
  };

  players.push(player);
}

const playersList = {
  players: players,
};

const playersJSON = JSON.stringify(playersList);

fs.writeFileSync("players.json", playersJSON);
