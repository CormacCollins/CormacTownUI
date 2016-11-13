using System;
using System.Collections.Generic;
using SwinGameSDK;


namespace MyGame
{
	public class UpdateGame
	{
        private ShowInfoBox _gameInfo;
		private ShowInfoBox _statsInfo;
		private ShowInfoBox _pauseGame;
		private bool _gameFinished;
		private Statistics _stats;
       

        public UpdateGame (EntityEnvironment e)
		{
			_gameFinished = false;
			
			_stats = new Statistics ();
			_stats.AddPreGameStats(e.GameEntities);
			
			
			// ------ PAUSE UI ------- //
			_pauseGame = new ShowInfoBox( 0, 0, (float)SwinGame.ScreenWidth()-200, (float)SwinGame.ScreenHeight());
            InGameLabel newLabel1 = new InGameLabel((_pauseGame.Width/2)-100, 250F, 200, 30, "Cormac Town");
            newLabel1.ChangeTextColor(Color.DarkRed);
            newLabel1.SetLabelColorRGBA(102, 102, 200, 255); 
			newLabel1.Padding = 50;
			_pauseGame.AddLabel(newLabel1);
			
			
            // ------ SETUP LABELS ---------- //
            // ------ Create InfoBox -------- //
            // ------ Create and add labels --//

            ShowInfoBox gameInformation = new ShowInfoBox((float)(SwinGame.ScreenWidth() - 200), 0, 200F, (float)SwinGame.ScreenHeight());
            InGameLabel pauseLabel = new InGameLabel(0F, 20F, 200, 30, "Cormac Town");
            newLabel1.ChangeTextColor(Color.DarkRed);
            newLabel1.SetLabelColorRGBA(102, 102, 200, 255); 
            gameInformation.AddLabel(pauseLabel);


            InGameLabel newLabel2 = new InGameLabel(0F, 55F, 200, 30, "Shapes alive: ");
            newLabel2.ChangeTextColor(Color.White);
            newLabel2.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel2);

            InGameLabel newLabel3 = new InGameLabel(0F, 85F, 200, 30, "Square Count: ");
            newLabel3.ChangeTextColor(Color.White);
            newLabel3.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel3);

            InGameLabel newLabel4 = new InGameLabel(0F, 115F, 200, 30, "Circle Count: ");
            newLabel4.ChangeTextColor(Color.White);
            newLabel4.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel4);

            InGameLabel newLabel5 = new InGameLabel(0F, 145F, 200, 30, "Triangle Count: ");
            newLabel5.ChangeTextColor(Color.White);
            newLabel5.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel5);

            InGameLabel newLabel6 = new InGameLabel(0F, 200F, 200, 30, "Entity: ");
            newLabel6.ChangeTextColor(Color.White);
            newLabel6.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel6);

            
            InGameLabel entLabel1 = new InGameLabel(25, 290, 150, 30, "Attractiveness: ");
            entLabel1.ChangeTextColor(Color.White);
            entLabel1.SetLabelColorRGBA(102, 102, 143, 255);
            gameInformation.AddLabel(entLabel1);

            InGameLabel entLabel2 = new InGameLabel(25, 330, 150, 30, "Fitness: ");
            entLabel2.ChangeTextColor(Color.White);
            entLabel2.SetLabelColorRGBA(102, 102, 143, 255);
            gameInformation.AddLabel(entLabel2);
			
			InGameLabel entLabel3 = new InGameLabel(25, 370, 150, 30, "Life: ");
            entLabel3.ChangeTextColor(Color.White);
            entLabel3.SetLabelColorRGBA(102, 102, 143, 255);
            gameInformation.AddLabel(entLabel3);
			
			//Entity selected draw position           
            gameInformation.EntityDisplayedY = 260;

			InGameLabel entLabel4 = new InGameLabel(25, 450, 150, 30, "PAUSE");
            entLabel4.ChangeTextColor(Color.White);
            entLabel4.SetLabelColorRGBA(0, 0, 0, 255);
            entLabel4.ButtonFlag = UIButtonFlags.pause;
			entLabel4.AlternateText = "RESUME";
			entLabel4.Padding = 50;
			gameInformation.AddLabel(entLabel4);
			
			InGameLabel entLabel5 = new InGameLabel(25, 490, 150, 30, "EXIT");
            entLabel5.ChangeTextColor(Color.White);
            entLabel5.SetLabelColorRGBA(255, 0, 0, 255);
			entLabel5.Padding = 50;
            entLabel5.ButtonFlag = UIButtonFlags.quit;
			gameInformation.AddLabel(entLabel5);
			
            //Add to field for rendering in Draw loop
            _gameInfo = gameInformation;
        }

		public void DrawEntities (EntityEnvironment e)
		{
			//Refresh gameData to be displayed
			_gameInfo.AddInformation ("Shapes alive", e.GameEntities.Count.ToString ());
			_gameInfo.AddInformation ("Square Count", e.Squares.ToString ());
			_gameInfo.AddInformation ("Circle Count", e.Circles.ToString ());
			_gameInfo.AddInformation ("Triangle Count", e.Triangles.ToString ());
			//Update the 'selected' entity display
			UpdateSelectedEntity (e);

			_gameInfo.SetEntity (e.SelectedEntity, 230F, _gameInfo.Width / 2);
			e.GameState = _gameInfo.GameState;
			
			
			CheckForGameEnd(e);
			// -------------- Check for game finished ------------ //
			
			
		
			

			// -------------------- DRAW INGAME UI ----------------------- //
			DrawGameInfo(e);
			
			//PAUSE
			if (!(e.GameState == UIButtonFlags.pause))
			{
				// --------------------- MAIN ENTITY LOOP ------------------------------------------------- //
				foreach (IGameObject g in e.GameEntities)
				{	
					e.UpdateEntities ();
					e.IsSelected (g as GameEntity);
							
					//No movement while 'Paused'
					Move.MovementChange (g, e);
				}
			}

            // --- Add new entities to map list --- //
            if (e.NewEntitiesToBeAdded != null)
            {
                //add new entities to our game intities
                foreach (IGameObject g in e.NewEntitiesToBeAdded)
                {
                    e.AddGameEntity(g);
                }
                //clear list as they are now added
                e.NewEntitiesToBeAdded.Clear();
            }

            //Remove any Entities the game intends to destroy
            if (e.ToDelete != null)
            {
                foreach (GameEntity g in e.ToDelete)
                {
                    int index = e.GameEntities.FindIndex(x => (x as GameEntity).ID == g.ID);
                    e.GameEntities.RemoveAt(index); //Can get quickler run time with refactoring the RemoveAll function - if time
                }
                e.ToDelete.Clear();
            }

           
		}


		
		
        //Gets information to populate the EntityGraphic on side
        public void UpdateSelectedEntity(EntityEnvironment e)
        {
            if (e.SelectedEntity == null) {           
                return;
            }
            int attrTrait = e.SelectedEntity.EntityGenes.ReturnGene("Attractiveness").GeneValue[0];
            int fitnessTrait = e.SelectedEntity.EntityGenes.ReturnGene("PhysicalStrength").GeneValue[0];
			int life = e.SelectedEntity.Life;
            _gameInfo.AddInformation("Attractiveness", attrTrait.ToString());
            _gameInfo.AddInformation("Fitness", fitnessTrait.ToString());			
			_gameInfo.AddInformation("Life", life.ToString());

        }

        public void GameFinishDisplay(EntityEnvironment e)
        {
			e.GameState = UIButtonFlags.pause;
        }
		
		public void CheckForGameEnd (EntityEnvironment e)
		{			
			if (e.GameState == UIButtonFlags.quit)
			{
				e.GameOver = true;
			}
			
			if ((e.GameEntities.Count >= 35) || (e.GameEntities.Count == 0))
			{
				_gameFinished = true;	
			}
		}
		
		public void DrawGameInfo (EntityEnvironment e)
		{			
			if (e.GameState == UIButtonFlags.pause)
			{
				_pauseGame.Draw();
			}			
			
			if (!_gameFinished)
			{
				_gameInfo.Draw ();
			}
			
			if (_gameFinished)
			{
				FinalizeGame (e);	
			}
			// ------------ DRAW GAME UI INFO / OR FINAL STATS DATA ------------- //
			
			if (_gameFinished)
			{
				_statsInfo.Draw ();
			}
			
		}
		
		
		public void FinalizeGame (EntityEnvironment e)
		{
			GameFinishDisplay (e);
			_gameFinished = true;
			//Prepare final stats to present
			_stats.CalculateStats ((e.GameEntities));
			

			//Infobox for game stats
			_statsInfo = new ShowInfoBox(0,  0, (SwinGame.ScreenWidth()), (SwinGame.ScreenHeight()));
			_statsInfo.BoxColor =  SwinGame.RGBAColor(123, 104, 238, 255);
			
			
			// ------------------------ CIRCLE -------------------------- //
			
			InGameLabel statLabel1 = new InGameLabel(0F, 5F, 300, 30, "Shape: Circle");
            statLabel1.ChangeTextColor(Color.White);
            statLabel1.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel1);
			
			InGameLabel statLabel2 = new InGameLabel(0F, 40F, 300, 30, "Starting percentage: " + _stats.Circles.PopulationPercentagePre.ToString("0.##\\%"));
            statLabel2.ChangeTextColor(Color.White);
            statLabel2.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel2);
			
			InGameLabel statLabel3 = new InGameLabel(0F, 75F, 300, 30, "Average Starting fitness: " + _stats.Circles.AvgFitnessPre.ToString());
            statLabel3.ChangeTextColor(Color.White);
            statLabel3.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel3);
			
			InGameLabel statLabel4 = new InGameLabel(0F, 105F, 300, 30, "Average Starting attractiveness: " + _stats.Circles.AvgAttractivenssPre.ToString());
            statLabel4.ChangeTextColor(Color.White);
            statLabel4.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel4);
			
			InGameLabel statLabelmutation = new InGameLabel(0, 135, 300, 30, "Mutations " + _stats.Circles.Mutations.ToString());
            statLabelmutation.ChangeTextColor(Color.White);
            statLabelmutation.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabelmutation);
			
			InGameLabel statLabel5 = new InGameLabel(300F, 40F, 300, 30, "Finishing percentage: " + _stats.Circles.PopulationPercentagePost.ToString("0.##\\%"));
            statLabel5.ChangeTextColor(Color.White);
            statLabel5.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel5);
			
			InGameLabel statLabel6 = new InGameLabel(300F, 75F, 300, 30, "Average Finishing fitness: " + _stats.Circles.AvgFitnessPost.ToString());
            statLabel6.ChangeTextColor(Color.White);
            statLabel6.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel6);
			
			InGameLabel statLabel7 = new InGameLabel(300F, 105F, 300, 30, "Average Finishing attractiveness: " + _stats.Circles.AvgAttractivenssPost.ToString());
            statLabel7.ChangeTextColor(Color.White);
            statLabel7.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel7);
			
			
			// ---------------------------- SQUARE -------------------------------- //
			
			InGameLabel statLabel8 = new InGameLabel(0, 185F, 200, 30, "Shape: Square");
            statLabel8.ChangeTextColor(Color.White);
            statLabel8.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel8);
			
			InGameLabel statLabel9 = new InGameLabel(0, 215F, 300, 30, "Starting percentage: " + _stats.Squares.PopulationPercentagePre.ToString("0.##\\%"));
            statLabel9.ChangeTextColor(Color.White);
            statLabel9.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel9);
			
			InGameLabel statLabel10 = new InGameLabel(0, 245F, 300, 30, "Average Starting fitness: " + _stats.Squares.AvgFitnessPre.ToString());
            statLabel10.ChangeTextColor(Color.White);
            statLabel10.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel10);
			
			InGameLabel statLabel11 = new InGameLabel(0, 275F, 300, 30, "Average Starting attractiveness: " + _stats.Squares.AvgAttractivenssPre.ToString());
            statLabel11.ChangeTextColor(Color.White);
            statLabel11.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel11);
			
			InGameLabel statLabelmutation1 = new InGameLabel(0, 305F, 300, 30, "Mutations " + _stats.Squares.Mutations.ToString());
            statLabelmutation1.ChangeTextColor(Color.White);
            statLabelmutation1.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabelmutation1);
			
			InGameLabel statLabel12 = new InGameLabel(300F, 215F, 300, 30, "Finishing percentage: " + _stats.Squares.PopulationPercentagePost.ToString("0.##\\%"));
            statLabel12.ChangeTextColor(Color.White);
            statLabel12.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel12);
			
			InGameLabel statLabel13 = new InGameLabel(300F, 245F, 300, 30, "Average Finishing fitness: " + _stats.Squares.AvgFitnessPost.ToString());
            statLabel13.ChangeTextColor(Color.White);
            statLabel13.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel13);
			
			InGameLabel statLabel14 = new InGameLabel(300F, 275F, 300, 30, "Average Finishing attractiveness: " + _stats.Squares.AvgAttractivenssPost.ToString());
            statLabel14.ChangeTextColor(Color.White);
            statLabel14.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel14);
			
			// ---------------------------- Triangle -------------------------------- //
			
			InGameLabel statLabel16 = new InGameLabel(0, 355F, 200, 30, "Shape: Triangle");
            statLabel16.ChangeTextColor(Color.White);
            statLabel16.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel16);
			
			InGameLabel statLabel17 = new InGameLabel(0, 385F, 300, 30, "Starting percentage: " + _stats.Trangles.PopulationPercentagePre.ToString("0.##\\%"));
            statLabel17.ChangeTextColor(Color.White);
            statLabel17.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel17);
			
			InGameLabel statLabel18 = new InGameLabel(0, 415F, 300, 30, "Average Starting fitness: " + _stats.Trangles.AvgFitnessPre.ToString());
            statLabel18.ChangeTextColor(Color.White);
            statLabel18.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel18);
			
			InGameLabel statLabel19 = new InGameLabel(0, 445F, 300, 30, "Average Starting attractiveness: " + _stats.Trangles.AvgAttractivenssPre.ToString());
            statLabel19.ChangeTextColor(Color.White);
            statLabel19.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel19);
			
			InGameLabel statLabelmutation2 = new InGameLabel(0, 475, 300, 30, "Mutations " + _stats.Trangles.Mutations.ToString());
            statLabelmutation2.ChangeTextColor(Color.White);
            statLabelmutation2.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabelmutation2);
			
			InGameLabel statLabel20 = new InGameLabel(300F, 385F, 300, 30, "Finishing percentage: " + _stats.Trangles.PopulationPercentagePost.ToString("0.##\\%"));
            statLabel20.ChangeTextColor(Color.White);
            statLabel20.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel20);
			
			InGameLabel statLabel21 = new InGameLabel(300F, 415F, 300, 30, "Average Finishing fitness: " + _stats.Trangles.AvgFitnessPost.ToString());
            statLabel21.ChangeTextColor(Color.White);
            statLabel21.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel21);
			
			InGameLabel statLabel22 = new InGameLabel(300F, 445F, 300, 30, "Average Finishing attractiveness: " + _stats.Trangles.AvgAttractivenssPost.ToString());
            statLabel22.ChangeTextColor(Color.White);
            statLabel22.SetLabelColorRGBA(220, 20, 60, 50);
            _statsInfo.AddLabel(statLabel22);
			
			
		}
		
		
		
	}
}

