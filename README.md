# Jeu-de-la-vie

*Conway's Game of Life with GUI*

**Date de réalisation :** Avril 2018

**Cadre du projet :** Cours "Tableaux et Algorithmes" en 1ère année de prépa intégrée à l'ESILV

**Langage utilisé :** C#

Le jeu de la vie a été imaginé par John Horton Conway vers 1970. C’est un « jeu à zéro joueur » car l’intervention d’aucun joueur n’est nécessaire à son déroulement. Le jeu de la vie se déroule sur une grille à deux dimensions, théoriquement infinie, composée de cases (appelées cellules) qui ont un état binaire (1 pour vivante et 0 pour morte).

Il s’agit d’un automate cellulaire qui repose sur le principe d’évolution de la grille dans le temps. A chaque étape, appelée génération, les cellules évoluent en fonction de leur voisinage et selon des règles spécifiques.

J’ai développé une variante du jeu de la vie avec deux populations de cellules distinctes évoluant sur la même grille. Par ailleurs, ne disposant pas d’une grille de dimension infinie, j’ai adopté des conventions pour obtenir une grille circulaire : les bords gauche et droit de la grille, ainsi que les bords haut et bas de la grille, sont connectés.
