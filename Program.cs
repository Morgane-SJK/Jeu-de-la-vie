using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsilvGui;

namespace Jeu_de_la_vie
{
    class Program
    {
        [System.STAThreadAttribute()]
        static void Main(string[] args)
        {
            JeuDeLaVie();
            Console.ReadKey();
        }


        static void JeuDeLaVie()
        {
            int nombreLignes = SaisirNombreLignes();
            int nombreColonnes = SaisirNombreColonnes();
            double tauxRemplissage = SaisirTauxRemplissage();
            int nombreCellulesVivantesAuDemarrage = Convert.ToInt32(tauxRemplissage * nombreLignes * nombreColonnes);
            int taillecellule = SaisirTailleCellule();
            Console.WriteLine("Vous voulez:");
            Console.WriteLine("1) un jeu DLV classique SANS visualisation intermédiaire des états futurs (taper 1)");
            Console.WriteLine("2) un jeu DLV classique AVEC visualisation intermédiaire des états futurs (taper 2)");
            Console.WriteLine("3) un jeu DLV variante (2 populations) SANS visualisation des états futurs (taper 3)");
            Console.WriteLine("4) un jeu DLV variante (2 populations) AVEC visualisation des états futurs (taper 4)");
            int type = Convert.ToInt32(Console.ReadLine());
            if (type == 1)
            {
                JeuDeLaVie1(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage, taillecellule);
            }
            if (type == 2)
            {
                JeuDeLaVie2(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage, taillecellule);
            }
            if (type == 3)
            {
                JeuDeLaVie3(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage, taillecellule);
            }
            if (type==4)
            {
                JeuDeLaVie4(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage, taillecellule);
            }
        }

        static int SaisirNombreLignes()
        {
            int nombreLignes = 0;
            do
            {
                Console.WriteLine("Quel est le nombre de lignes de la grille ? ");
                try
                {
                    nombreLignes = Convert.ToInt32(Console.ReadLine());  //teste si on peut convertir le nombre saisi en entier
                }
                catch
                {
                    Console.WriteLine("Merci de rentrer un nombre entier > 0");
                }
            } while (nombreLignes <= 0);
            return nombreLignes;
        }

        static int SaisirNombreColonnes()
        {
            int nombreColonnes = 0;
            do
            {
                Console.WriteLine("Quel est le nombre de colonnes de la grille ? ");
                try
                {
                    nombreColonnes = Convert.ToInt32(Console.ReadLine());  //teste si on peut convertir le nombre saisi en entier
                }
                catch
                {
                    Console.WriteLine("Merci de rentrer un nombre entier > 0");
                }
            } while (nombreColonnes <= 0);
            return nombreColonnes;
        }

        static double SaisirTauxRemplissage()
        {
            double tauxRemplissage = 0;
            do
            {
                Console.WriteLine("Quel est le taux de remplissage de cellules vivantes au départ ? (donner un réel compris entre 0,1 et 0,9)");
                try
                {
                    tauxRemplissage = Convert.ToDouble(Console.ReadLine()); //teste si on peut convertir le nombre saisi en double
                }
                catch
                {
                    Console.WriteLine("Merci de rentrer un nombre compris entre 0,1 et 0,9");
                }
            } while ((tauxRemplissage < 0.1) || (tauxRemplissage > 0.9));
            return tauxRemplissage;
        }

        static int SaisirTailleCellule()
        {
            int taillecellule = 0;
            do
            {
                Console.WriteLine("Quelle est la taille d'une cellule ? (en pixels)");
                try
                {
                    taillecellule = Convert.ToInt32(Console.ReadLine());  //teste si on peut convertir le nombre saisi en entier
                }
                catch
                {
                    Console.WriteLine("Merci de rentrer un nombre entier > 0");
                }
            } while (taillecellule <= 0);
            return taillecellule;
        }

        static int[,] CreationGrille(int nombreLignes, int nombreColonnes, int nombreCellulesVivantesAuDemarrage)
        {
            Random generateur = new Random(); //générateur aléatoire
            int[,] grille = new int[nombreLignes, nombreColonnes];
            int[] positionCellulesVivantes = new int[nombreCellulesVivantesAuDemarrage]; //tableau qui va contenir les positions des cellules vivantes
            int position;
            int positionCourante = 0;
            bool celluleVivante;
            bool celluleOccupee;

            // Initialisation du tableau avec des -1
            for (int i = 0; i < nombreCellulesVivantesAuDemarrage; i++)
            {
                positionCellulesVivantes[i] = -1;
            }

            // Génération aléatoire des positions des cellules vivantes
            for (int i = 0; i < nombreCellulesVivantesAuDemarrage; i++)
            {
                position = generateur.Next(1, (nombreLignes * nombreColonnes) + 1); //position appartient à [1,*]
                position--; //on aura donc une position qui appartient à l'intervalle [0,*-1]
                // Verifier que cette position n'est pas déjà occupée par une cellule vivante
                celluleOccupee = false;
                for (int j = 0; j < nombreCellulesVivantesAuDemarrage; j++)
                {
                    if (positionCellulesVivantes[j] == position)
                    {
                        celluleOccupee = true;
                        break;
                    }
                }

                if (!celluleOccupee)
                {
                    positionCellulesVivantes[i] = position;
                }
                else
                {
                    // refaire un tour supplémentaire car position déjà   occupée par une cellule vivante
                    i--;
                }
            }


            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    // Est-ce que ma position courante est une cellule vivante ?
                    celluleVivante = false;
                    for (int k = 0; k < nombreCellulesVivantesAuDemarrage; k++)
                    {
                        if (positionCellulesVivantes[k] == positionCourante)
                        {
                            celluleVivante = true;
                            break;
                        }
                    }
                    if (celluleVivante)
                    {
                        grille[i, j] = 1;
                    }
                    else
                    {
                        grille[i, j] = 0;
                    }

                    positionCourante = positionCourante + 1;
                }
            }
            return grille;
        }

        static void AfficherGrille(int[,] grille)
        {
            if (grille == null)
            {
                Console.WriteLine("grille null");
            }
            else
            {
                if ((grille.GetLength(0) == 0) || (grille.GetLength(1) == 0))
                {
                    Console.WriteLine("grille vide");
                }
                else
                {
                    for (int i = 0; i < grille.GetLength(0); i++)
                    {
                        for (int j = 0; j < grille.GetLength(1); j++)
                        {
                            if (grille[i, j] == 0)
                            {
                                Console.Write(" . ");
                            }
                            if (grille[i, j] == 1)
                            {
                                Console.Write(" # ");
                            }
                            if (grille[i, j] == 2)
                            {
                                Console.Write(" - ");
                            }
                            if (grille[i, j] == 3)
                            {
                                Console.Write(" * ");
                            }
                            if (grille[i, j] == 4)
                            {
                                Console.Write(" @ ");
                            }
                            if (grille[i, j] == 5)
                            {
                                Console.Write("_");
                            }
                            if (grille[i, j] == 6)
                            {
                                Console.Write(" ° ");
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        static int CelluleVivanteInt(int[,] grille, int i, int j)  //Fonction qui attribue la valeur 1 à une cellule vivante et 0 à une cellule morte
        {
            int cellulevivanteInt = 0;
            int nbLignes = grille.GetLength(0);
            int nbColonnes = grille.GetLength(1);

            // Tester si on doit prendre la case circulaire
            if (i < 0)
            {
                i = nbLignes - 1;
            }
            if (j < 0)
            {
                j = nbColonnes - 1;
            }
            if (i >= nbLignes)
            {
                i = 0;
            }
            if (j >= nbColonnes)
            {
                j = 0;
            }
            // cellule vivante ou à mourir
            if ((grille[i, j] == 1) || (grille[i, j] == 3))
            {
                cellulevivanteInt = 1;
            }
            return cellulevivanteInt;
        }

        static bool CelluleDevientVivante(int[,] grille, int i, int j) //Fonction qui décide si une fonction devient vivante ou morte en fonction du nombre de cellules vivantes qui l'entourent
        {
            int nombrecellulesvivantesautour = 0;
            bool celluledevientvivante = false;
            nombrecellulesvivantesautour = CelluleVivanteInt(grille, i - 1, j - 1) +
                                           CelluleVivanteInt(grille, i - 1, j) +
                                           CelluleVivanteInt(grille, i - 1, j + 1) +
                                           CelluleVivanteInt(grille, i, j - 1) +
                                           CelluleVivanteInt(grille, i, j + 1) +
                                           CelluleVivanteInt(grille, i + 1, j - 1) +
                                           CelluleVivanteInt(grille, i + 1, j) +
                                           CelluleVivanteInt(grille, i + 1, j + 1);

            if (CelluleVivanteInt(grille, i, j) == 1) //cellule [i,j] vivante
            {
                if ((nombrecellulesvivantesautour == 2) || (nombrecellulesvivantesautour == 3))
                {
                    celluledevientvivante = true; //la cellule reste vivante
                }
            }
            else //cellule [i,j] morte
            {
                if (nombrecellulesvivantesautour == 3)
                {
                    celluledevientvivante = true; //la cellule devient vivante
                }
            }
            return celluledevientvivante;

        }

        static int TaillePopulation(int[,] grille) //Fonction qui compte le nombre de cellules vivantes (de la population 1 pour l'étape 2)
        {
            int taillepopulation = 0;
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] == 1)
                    {
                        taillepopulation = taillepopulation + 1;
                    }
                }
            }
            return taillepopulation;
        }

        static void JeuDeLaVie1(int nombreLignes, int nombreColonnes, int nombreCellulesVivantesAuDemarrage, int taillecellule)

        {
            int[,] grille = CreationGrille(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage);
            Fenetre gui = new Fenetre(grille, taillecellule, 0, 0, "Génération : 0");
            gui.RafraichirTout();
            gui.changerMessage("Génération : 0 " + "  Nombre de cellules vivantes : " + TaillePopulation(grille));
            Console.WriteLine("Génération : 0");
            AfficherGrille(grille);
            Console.WriteLine("Nombre de cellules vivantes : " + TaillePopulation(grille));
            Console.ReadLine();
            Console.WriteLine();
            for (int k = 1; k < 20; k++)
            {
                Console.WriteLine("Génération : " + k);
                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if ((CelluleDevientVivante(grille, i, j)) && (grille[i, j] == 0))
                        {
                            // une cellule morte => une cellule à naître
                            grille[i, j] = 2;
                        }
                        if ((!CelluleDevientVivante(grille, i, j)) && (grille[i, j] == 1))
                        {
                            // une cellule vivante => une cellule à mourir
                            grille[i, j] = 3;
                        }
                    }
                }
                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if (grille[i, j] == 2)
                        {
                            // une cellule à naître => cellule vivante
                            grille[i, j] = 1;
                        }
                        if (grille[i, j] == 3)
                        {
                            // une cellule à mourir => cellule morte
                            grille[i, j] = 0;
                        }
                    }
                }
                AfficherGrille(grille);
                gui.RafraichirTout();
                gui.changerMessage("Génération : " + k + "  Nombre de cellules vivantes : " + TaillePopulation(grille));

                Console.WriteLine("Nombre de cellules vivantes : " + TaillePopulation(grille));
                Console.ReadLine();
                Console.WriteLine();
            }
        }

        static void JeuDeLaVie2(int nombreLignes, int nombreColonnes, int nombreCellulesVivantesAuDemarrage, int taillecellule)
        {
            int[,] grille = CreationGrille(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage);
            Fenetre gui = new Fenetre(grille, taillecellule, 0, 0, "Génération : 0");

            for (int k = 0; k < 20; k++)
            {
                gui.RafraichirTout();
                gui.changerMessage("Génération : " + k + "  Nombre de cellules vivantes : " + TaillePopulation(grille));
                Console.WriteLine("Génération : " + k);
                AfficherGrille(grille);
                Console.WriteLine("Nombre de cellules vivantes : " + TaillePopulation(grille));
                Console.ReadLine();
                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if ((CelluleDevientVivante(grille, i, j)) && (grille[i, j] == 0))
                        {
                            // une cellule morte => une cellule à naître
                            grille[i, j] = 2;
                        }
                        if ((!CelluleDevientVivante(grille, i, j)) && (grille[i, j] == 1))
                        {
                            // une cellule vivante => une cellule à mourir
                            grille[i, j] = 3;
                        }
                    }
                }
                Console.WriteLine("Génération : " + k + "+");
                AfficherGrille(grille);
                gui.RafraichirTout();
                gui.changerMessage("Génération : " + k + " + ");

                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if (grille[i, j] == 2)
                        {
                            // cellule à naître =>  cellule vivante
                            grille[i, j] = 1;
                        }
                        if (grille[i, j] == 3)
                        {
                            // cellule à mourir => cellule morte
                            grille[i, j] = 0;
                        }
                    }
                }
                Console.ReadLine();
                Console.WriteLine();
            }
        }

        static int[,] CreationGrille2(int nombreLignes, int nombreColonnes, int nombreCellulesVivantesAuDemarrage)
        {
            int[,] grille = new int[nombreLignes, nombreColonnes];
            int nombredecellules1 = Convert.ToInt32(nombreCellulesVivantesAuDemarrage / 2); //nombre de cellules de la population 1
            int nombredecellules2 = (nombreLignes*nombreColonnes) - nombredecellules1;  //nombre de cellules de la population 2

            Random generateur = new Random(); //générateur aléatoire
            int[] positionCellulesVivantes = new int[nombreCellulesVivantesAuDemarrage]; //tableau qui va contenir les positions des cellules vivantes de la population 1 et 2
            int position;
            int positionCourante = 0;
            bool celluleVivante1;
            bool celluleVivante2;
            bool celluleOccupee;

            // Initialisation du tableau avec des -1
            for (int i = 0; i < nombreCellulesVivantesAuDemarrage; i++)
            {
                positionCellulesVivantes[i] = -1;
            }

            // Génération aléatoire des positions des cellules vivantes de la population 1 et 2
            for (int i = 0; i < nombreCellulesVivantesAuDemarrage; i++)
            {
                position = generateur.Next(1, (nombreLignes * nombreColonnes) + 1); //position appartient à [1,*]
                position--; //on aura donc une position qui appartient à l'intervalle [0,*-1]
                // Verifier que cette position n'est pas déjà occupée par une cellule vivante
                celluleOccupee = false;
                for (int j = 0; j < nombreCellulesVivantesAuDemarrage; j++)
                {
                    if (positionCellulesVivantes[j] == position)
                    {
                        celluleOccupee = true;
                        break;
                    }
                }

                if (!celluleOccupee)
                {
                    positionCellulesVivantes[i] = position;
                }
                else
                {
                    // refaire un tour supplémentaire car position déjà occupée par une cellule vivante
                    i--;
                }
            }

            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    // Est-ce que ma position courante est une cellule vivante de la population 1?
                    celluleVivante1 = false;
                    for (int k = 0; k < nombredecellules1; k++)
                    {
                        if (positionCellulesVivantes[k] == positionCourante)
                        {
                            celluleVivante1 = true;
                            break;
                        }
                    }
                    // Est-ce que ma position courante est une cellule vivante de la population 2?
                    celluleVivante2 = false;
                    for (int k = nombredecellules1; k < nombreCellulesVivantesAuDemarrage; k++)
                    {
                        if (positionCellulesVivantes[k] == positionCourante)
                        {
                            celluleVivante2 = true;
                            break;
                        }
                    }
                    if (celluleVivante1)
                    {
                        grille[i, j] = 1;
                    }  else if (celluleVivante2)
                    {
                         grille[i, j] = 4;
                    }
                    else
                    {
                        grille[i, j] = 0;
                    }
                    positionCourante = positionCourante + 1;
                }
            }
            return grille;
        }

        static int CelluleVivanteInt2(int [,] grille, int i, int j, int population)
        {
            int cellulevivanteInt2 = 0;
            int nbLignes = grille.GetLength(0);
            int nbColonnes = grille.GetLength(1);

            // Tester si on doit prendre la case circulaire
            if (i == -1)
            {
                i = nbLignes - 1;
            }
            if (i==-2)
            {
                i = nbLignes - 2;
            }
            if (j == -1)
            {
                j = nbColonnes - 1;
            }
            if (j==-2)
            {
                j = nbColonnes - 2;
            }
            if (i == nbLignes)
            {
                i = 0;
            }
            if (i==nbLignes+1)
            {
                i = 1;
            }
            if (j == nbColonnes)
            {
                j = 0;
            }
            if (j==nbColonnes+1)
            {
                j = 1;
            }
            // cellule vivante ou à mourir
            if (population==1)
            {

                if ((grille[i, j] == 1) || (grille[i, j] == 3))
                {
                    cellulevivanteInt2 = 1;
                }
            }
            if (population ==2)
            {
                if ((grille[i, j] == 4) || (grille[i, j] == 6))
                {
                    cellulevivanteInt2 = 1;
                }
            }
            return cellulevivanteInt2;
        }

        static int NombreCellulesVivantesAutourRang1(int [,] grille, int i, int j,int population)  //Fonction qui calcule le nombre de cellules vivantes d'une certaine population autour d'une cellule
        {
            int nombrecellulesvivantesautourrang1 = 0;
            nombrecellulesvivantesautourrang1 = CelluleVivanteInt2(grille, i - 1, j - 1,population) +
                               CelluleVivanteInt2(grille, i - 1, j,population) +
                               CelluleVivanteInt2(grille, i - 1, j + 1,population) +
                               CelluleVivanteInt2(grille, i, j - 1,population) +
                               CelluleVivanteInt2(grille, i, j + 1,population) +
                               CelluleVivanteInt2(grille, i + 1, j - 1,population) +
                               CelluleVivanteInt2(grille, i + 1, j,population) +
                               CelluleVivanteInt2(grille, i + 1, j + 1,population);
            return nombrecellulesvivantesautourrang1;
        }

        static int NombreCellulesVivantesAutourRang2(int [,] grille, int i, int j, int population)  //Fonction qui calcule le nombre de cellules vivantes d'une certaine population au voisinage de rang 2 d'une cellule
        {
            int nombrecellulesvivantesautourrang2 = 0;
            nombrecellulesvivantesautourrang2 = NombreCellulesVivantesAutourRang1(grille,i,j,population) + CelluleVivanteInt2(grille, i - 2, j - 2,population) +
                                    CelluleVivanteInt2(grille, i - 2, j - 1,population) + CelluleVivanteInt2(grille, i - 2, j,population) +
                                    CelluleVivanteInt2(grille, i - 2, j + 1,population) + CelluleVivanteInt2(grille, i - 2, j + 2,population) +
                                    CelluleVivanteInt2(grille, i - 1, j - 2,population) + CelluleVivanteInt2(grille, i - 1, j + 2,population) +
                                    CelluleVivanteInt2(grille, i, j - 2,population) + CelluleVivanteInt2(grille, i, j + 2,population) +
                                    CelluleVivanteInt2(grille, i + 1, j - 2,population) + CelluleVivanteInt2(grille, i + 1, j + 2,population) +
                                    CelluleVivanteInt2(grille, i + 2, j - 2,population) + CelluleVivanteInt2(grille, i + 2, j - 1,population) +
                                    CelluleVivanteInt2(grille, i + 2, j,population) + CelluleVivanteInt2(grille, i + 2, j + 1,population) +
                                    CelluleVivanteInt2(grille, i + 2, j + 2,population);
            return nombrecellulesvivantesautourrang2;
        }

        static bool CelluleDevientVivantePop1(int [,] grille, int i, int j)
        {
            int nombrecellulesvivantesautourPop1rang1 = NombreCellulesVivantesAutourRang1(grille,i,j,1);
            int nombrecellulesvivantesautourPop1rang2 = NombreCellulesVivantesAutourRang2(grille,i,j,1);
            int nombrecellulesvivantesautourPop2rang1 = NombreCellulesVivantesAutourRang1(grille, i, j,2);
            int nombrecellulesvivantesautourPop2rang2 = NombreCellulesVivantesAutourRang2(grille, i, j,2);
            int taillepopulation1 = TaillePopulation(grille);
            int taillepopulation2 = TaillePopulation2(grille);
            bool celluledevientvivantepop1 = false;

            if (CelluleVivanteInt2(grille, i, j,1) == 1) //cellule [i,j] vivante de population 1
            {
                if ((nombrecellulesvivantesautourPop1rang1 == 2) || (nombrecellulesvivantesautourPop1rang1 == 3))
                {
                    celluledevientvivantepop1 = true; //la cellule reste vivante
                }
            }
            if(CelluleVivanteInt2(grille,i,j,1)==0) //cellule [i,j] morte
            {
                if ((nombrecellulesvivantesautourPop1rang1 == 3)&&(nombrecellulesvivantesautourPop2rang1!=3))
                {
                    celluledevientvivantepop1 = true; //la cellule devient vivante de population 1
                }
                if ((nombrecellulesvivantesautourPop1rang1==3)&&(nombrecellulesvivantesautourPop2rang1==3))
                {
                    if (nombrecellulesvivantesautourPop1rang2 > nombrecellulesvivantesautourPop2rang2)
                    {
                        celluledevientvivantepop1=true;
                    }
                    if (nombrecellulesvivantesautourPop1rang2==nombrecellulesvivantesautourPop2rang2)
                    {
                        if(taillepopulation1>taillepopulation2)
                        {
                            celluledevientvivantepop1 = true;
                        }
                        if(taillepopulation1==taillepopulation2)
                        {
                            celluledevientvivantepop1 = false;
                        }
                    }
                }
            }
            return celluledevientvivantepop1;
        }

        static bool CelluleDevientVivantePop2(int[,] grille, int i, int j)
        {
            int nombrecellulesvivantesautourPop1rang1 = NombreCellulesVivantesAutourRang1(grille, i, j,1);
            int nombrecellulesvivantesautourPop1rang2 = NombreCellulesVivantesAutourRang2(grille, i, j,1);
            int nombrecellulesvivantesautourPop2rang1 = NombreCellulesVivantesAutourRang1(grille, i, j,2);
            int nombrecellulesvivantesautourPop2rang2 = NombreCellulesVivantesAutourRang2(grille, i, j,2);
            int taillepopulation1 = TaillePopulation(grille);
            int taillepopulation2 = TaillePopulation2(grille);
            bool celluledevientvivantepop2 = false;

            if (CelluleVivanteInt2(grille, i, j,2) == 1) //cellule [i,j] vivante de population 2
            {
                if ((nombrecellulesvivantesautourPop2rang1 == 2) || (nombrecellulesvivantesautourPop2rang1 == 3))
                {
                    celluledevientvivantepop2 = true; //la cellule reste vivante
                }
            }
            if (CelluleVivanteInt2(grille, i, j,2) == 0) //cellule [i,j] morte
            {
                if ((nombrecellulesvivantesautourPop2rang1 == 3) && (nombrecellulesvivantesautourPop1rang1 != 3))
                {
                    celluledevientvivantepop2 = true; //la cellule devient vivante de population 2
                }
                if ((nombrecellulesvivantesautourPop2rang1 == 3) && (nombrecellulesvivantesautourPop1rang1 == 3))
                {
                    if (nombrecellulesvivantesautourPop2rang2 > nombrecellulesvivantesautourPop1rang2)
                    {
                        celluledevientvivantepop2 = true;
                    }
                    if (nombrecellulesvivantesautourPop2rang2 == nombrecellulesvivantesautourPop1rang2)
                    {
                        if (taillepopulation2 > taillepopulation1)
                        {
                            celluledevientvivantepop2 = true;
                        }
                        if (taillepopulation2 == taillepopulation1)
                        {
                            celluledevientvivantepop2 = false;
                        }
                    }
                }
            }
            return celluledevientvivantepop2;
        }

        static int TaillePopulation2(int[,] grille)  //Fonction qui compte le nombre de cellules vivantes de la population 1
        {
            int taillePopulation2 = 0;
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    if (grille[i, j] == 4)
                    {
                        taillePopulation2 = taillePopulation2 + 1;
                    }
                }
            }
            return taillePopulation2;
        }

        static void JeuDeLaVie3(int nombreLignes, int nombreColonnes, int nombreCellulesVivantesAuDemarrage, int taillecellule)
        {
            int[,] grille = CreationGrille2(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage);
            Fenetre gui = new Fenetre(grille, taillecellule, 0, 0, "Génération : 0");
            gui.RafraichirTout();
            gui.changerMessage("Génération : 0 " + "  Population 1 : " + TaillePopulation(grille) + "  Population 2 : "+ TaillePopulation2(grille));
            Console.WriteLine("Génération : 0");
            AfficherGrille(grille);
            Console.WriteLine("Population 1 : " + TaillePopulation(grille));
            Console.WriteLine("Population 2 : " + TaillePopulation2(grille));
            Console.ReadLine();
            Console.WriteLine();
            for (int k = 1; k < 20; k++)
            {
                Console.WriteLine("Génération : " + k);
                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if (CelluleDevientVivantePop1(grille,i,j)&&(grille[i,j]==0)) //cellule morte devient vivante de population 1
                        {
                            grille[i, j] = 2;
                        }
                        if (CelluleDevientVivantePop2(grille,i,j)&&(grille[i,j]==0)) //cellule morte devient vivante de population 2
                        {
                            grille[i, j] = 5;
                        }
                        if (!CelluleDevientVivantePop1(grille,i,j)&&(grille[i,j]==1)) //cellule vivante de la population 1 devient morte
                        {
                            grille[i, j] = 3;
                        }
                        if (!CelluleDevientVivantePop2(grille, i, j) && (grille[i, j] == 4))  //cellule vivante de la population 2 devient morte
                        {
                            grille[i, j] = 6;
                        }
                    }
                }
                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if (grille[i, j] == 2)
                        {
                            // une cellule à naître => cellule vivante de population 1
                            grille[i, j] = 1;
                        }
                        if (grille[i, j] == 5)
                        {
                            //une cellule à naître => cellule vivante de population 2
                            grille[i, j] = 4;
                        }
                        if ((grille[i, j] == 3)||(grille[i,j]==6))
                        {
                            // une cellule à mourir => cellule morte
                            grille[i, j] = 0;
                        }
                    }
                }
                AfficherGrille(grille);
                gui.RafraichirTout();
                gui.changerMessage("Génération : " + k + "  Population 1 : " + TaillePopulation(grille) + "  Population 2 : " + TaillePopulation2(grille));
                Console.WriteLine("Population 1 : " + TaillePopulation(grille));
                Console.WriteLine("Population 2 : " + TaillePopulation2(grille));
                Console.ReadLine();
                Console.WriteLine();
            }
        }

        static void JeuDeLaVie4(int nombreLignes, int nombreColonnes, int nombreCellulesVivantesAuDemarrage, int taillecellule)
        {
            int[,] grille = CreationGrille2(nombreLignes, nombreColonnes, nombreCellulesVivantesAuDemarrage);
            Fenetre gui = new Fenetre(grille, taillecellule, 0, 0, "Génération : 0");

            for (int k = 0; k < 20; k++)
            {
                gui.RafraichirTout();
                gui.changerMessage("Génération : " + k + "  Population 1 : " + TaillePopulation(grille) +"  Population 2 : "+TaillePopulation2(grille));
                Console.WriteLine("Génération : " + k);
                AfficherGrille(grille);
                Console.WriteLine("Population 1 : " + TaillePopulation(grille));
                Console.WriteLine("Population 2 : " + TaillePopulation2(grille));
                Console.ReadLine();
                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if (CelluleDevientVivantePop1(grille, i, j) && (grille[i, j] == 0)) //cellule morte devient vivante de population 1
                        {
                            grille[i, j] = 2;
                        }
                        if (CelluleDevientVivantePop2(grille, i, j) && (grille[i, j] == 0)) //cellule morte devient vivante de population 2
                        {
                            grille[i, j] = 5;
                        }
                        if (!CelluleDevientVivantePop1(grille, i, j) && (grille[i, j] == 1)) //cellule vivante de la population 1 devient morte
                        {
                            grille[i, j] = 3;
                        }
                        if (!CelluleDevientVivantePop2(grille,i,j)&&(grille[i,j]==4))  //cellule vivante de la population 2 devient morte
                        {
                            grille[i, j] = 6;
                        }
                    }
                }
                Console.WriteLine("Génération : " + k + "+");
                AfficherGrille(grille);
                gui.RafraichirTout();
                gui.changerMessage("Génération : " + k + " + ");

                for (int i = 0; i < nombreLignes; i++)
                {
                    for (int j = 0; j < nombreColonnes; j++)
                    {
                        if (grille[i, j] == 2)
                        {
                            // une cellule à naître => cellule vivante de population 1
                            grille[i, j] = 1;
                        }
                        if (grille[i, j] == 5)
                        {
                            //une cellule à naître => cellule vivante de population 2
                            grille[i, j] = 4;
                        }
                        if ((grille[i, j] == 3) || (grille[i, j] == 6))
                        {
                            // une cellule à mourir => cellule morte
                            grille[i, j] = 0;
                        }
                    }
                }
                Console.ReadLine();
                Console.WriteLine();
            }
        }
    }

}
