# O Labirinto de Konigsberg

[Link do Recurso Educacional Aberto (REA)](https://apps.univesp.br/labirinto-de-konigsberg/)

O objetivo desse recurso é ser uma experiência interativa sobre um caso famoso que aconteceu na cidade de Konigsberg, atual Kaliningrado, na Rússia. A cidade é cortada pelo Rio Pregel, onde há duas grandes ilhas que, juntas, formam um complexo que na época continha sete pontes.

O sistema programado nesse jogo pode ser adaptado de diversas formas, como:

1. Mudar a aparência do jogo
2. Incluir mais pontes para dificultar o enigma

Para editar esse REA, você precisará estar familiarizado com Unity3D e C#.

*Esse jogo foi desenvolvido em Unity3d 2019.3.2f1. Rodar esse projeto em outras versões da Unity pode causar erros que podem impedir o funcionamento correto do mesmo.

- - - -

### 1. Mudar a aparência do jogo

No caso de mudar os sprites do cenário e das pontes, provavelmente você terá que atualizar a rota que o personagem percorre quando você clica na ponte. Para isso você precisa pegar as coordenadas usando o próprio modelo do personagem como base e criar GameObjects que servirão como Waypoints. Se não souber como trabalhar com waypoints usando gameobjects, dê uma olhada nesse breve tutorial: https://jogoscomcafe.wordpress.com/2019/05/09/tutorial-waypoints-simples-em-2d-unity/
Todos os waypoints estão separados por ponte no objeto 3D Plan > Waypoints. Cada grupo de waypoint é colocado via Inspector no script de cada ponte correspondente. As pontes estão no objeto Scenery.Canvas > Scenery. Lembre-se que a ordem das coordenadas é sempre no sentido da ilha central.

Se for mudar o personagem principal, você deve colocá-lo como filho do objeto 3D Plan > Player True Angles.

### 2. Incluir mais pontes para dificultar o enigma

Cada ponte tem um componente Button que chama dois métodos: 
- Scenery.Canvas -> BridgeCentral.SetCurrentBridge (você passa essa mesma ponte como parâmetro)
- @Core -> AudioPlayer.PlaySFX (você passa o som de clique como parâmetro)
Cada ponte também recebe um script Bridge, que recebe os seguintes dados:
- Waypoints -> lista de waypoints relacionados a essa ponte
- Inner Island -> são as ilhas centrais. É preciso passar um ID relacionado a essa ilha
- Outer Island -> são as ilhas próximas da borda da tela. É preciso passar um ID relacionado a essa ilha
- Inner Bridges -> são as pontes que tem contato com a ilha central dessa ponte
- Outer Bridges -> são as pontes que tem contato com a ilha externa dessa ponte
- Bridge Image -> é o componente Image desse mesmo objeto
- Bridge Button -> é o componente Button dessa mesma imagem
- Disabled Sprite -> é a imagem da ponte quebrada
- Break SFX -> é o som da ponte quebrada

Para que o jogo reconheça a quantidade certa de pontes no final do jogo, você deve atribuir a quantidade atual de pontes na linha 93 do script GameState.cs
