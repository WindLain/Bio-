using System.Reflection;
using UnityEngine;

namespace Bio {
public class SCS_Gene : MonoBehaviour
{
    private const float mutationChance = 0.5f;
        private readonly AnimalStats stats;

        public SCS_Gene(AnimalStats stats)
        {
            this.stats = stats;
        }

        public AnimalStats GetInheritedGenes(SCS_Gene father, SCS_Gene mother)
        {
            float speed = Random.value >= 0.5f ? father.stats.MovementSpeed : mother.stats.MovementSpeed;
            if (Random.value >= mutationChance)
                speed = MutateGene(speed);

            float size = Random.value >= 0.5f ? father.stats.Size : mother.stats.Size;
            if (Random.value >= mutationChance)
                size = MutateGene(size);

            float lineViewRadius = Random.value >= 0.5f ? father.stats.LineViewRadius : mother.stats.LineViewRadius;
            if (Random.value >= mutationChance)
                lineViewRadius = MutateGene(lineViewRadius);


            Color cvet = Random.value >= 0.5f ? father.stats.Cvet : mother.stats.Cvet;
            if (Random.value >= mutationChance)
                lineViewRadius = MutateGene(lineViewRadius);

            AnimalStats test = new AnimalStats(size, speed, lineViewRadius, cvet);
            return test;
        }//endAnimalStats

        public float MutateGene(float geneValue)
        {
            geneValue += Random.Range(-0.3f, 0.3f);
            return geneValue;
        }

        public Color MutateColor(Color geneValue)
        {
            geneValue.r += Random.Range(-0.3f, 0.3f);
            geneValue.g += Random.Range(-0.3f, 0.3f);
            geneValue.b += Random.Range(-0.3f, 0.3f);

            return geneValue;//endpubeValue;
        }

    }//end
}//namespace