using System;

namespace MyGame
{
	public struct StatsStruct
	{
		string _type;
		float _populationPercentagePost;
		float _populationPercentagePre;
		bool _isTheDominant;
		float _avgFitnessPre;
		float _avgFitnessPost;
		float _avgAttractivenssPre;
		float _avgAttractivenssPost;
		int _mutations;	


		public float PopulationPercentagePre{
			get{return _populationPercentagePre;}
			set{_populationPercentagePre = value*100;}
		}

		public float AvgAttractivenssPost{
			get{return _avgAttractivenssPost; }
			set{ _avgAttractivenssPost = value;}
		}

		public float AvgAttractivenssPre{
			get{return _avgAttractivenssPre;}
			set{_avgAttractivenssPre = value;}
		}

		public float AvgFitnessPost{
			get{return _avgFitnessPost;}
			set{_avgFitnessPost = value;}
		}

		public float AvgFitnessPre{
			get{return _avgFitnessPre;}
			set{_avgFitnessPre = value;}
		}

		public string Type{
			get{return _type;}
			set{_type = value;}
		}

		public float PopulationPercentagePost{
			get{return _populationPercentagePost;}
			set{_populationPercentagePost = value*100;}
		}

		public bool IsTheDominant{
			get{return _isTheDominant;}
			set{_isTheDominant = value;}
		}
		
		public int Mutations{
			get{return _mutations;}
			set{_mutations = value;}
		}
	}
}

