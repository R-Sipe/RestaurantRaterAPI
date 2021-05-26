using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        public double Rating
        {
            get
            {
                double totalAverageRating = 0;

                foreach (Rating rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }
                return totalAverageRating / Ratings.Count;
            }
        }

        //AverageFoodScore
        public double FoodRating
        {
            get
            {
                double totalFoodScore = 0;

                foreach (var rating in Ratings)
                {
                    totalFoodScore += rating.FoodScore;
                }
                return (Ratings.Count > 0) ? totalFoodScore / Ratings.Count : 0;
            }
        }

        //AverageEnviormentScore
        public double EnviormentRating
        {
            get
            {
                IEnumerable<double> scores = Ratings.Select(rating => rating.EnviormentScore);

                double totalEnviormentScore = scores.Sum();

                return (Ratings.Count > 0) ? totalEnviormentScore / Ratings.Count() : 0;
            }
        }

        //AverageCleanlinessScore
        public double CleanlinessScore
        {
            get
            {
                var totalScore = Ratings.Select(r => r.CleanlinessScore).Sum();
                return (Ratings.Count > 0) ? totalScore / Ratings.Count : 0;
            }
        }

        public bool IsRecommended => Rating > 8.5;
    }
}