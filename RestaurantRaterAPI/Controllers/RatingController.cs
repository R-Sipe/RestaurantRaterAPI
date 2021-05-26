using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostRating(Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Restaurant restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest();
            }

            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"You rated {restaurant.Name} successfully");
            }

            return InternalServerError();

        }
        //Update Rating
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating(int id, Rating newRating)
        {
            Rating rating = await _context.Ratings.FindAsync(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (rating != null)
            {
                rating.CleanlinessScore = newRating.CleanlinessScore;
                rating.EnviormentScore = newRating.EnviormentScore;
                rating.FoodScore = newRating.FoodScore;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        //Delete Rating
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRating(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            _context.Ratings.Remove(rating);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The rating was deleted.");
            }

            return InternalServerError();

        }

        //GetAllRatings?
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRatings()
        {
            List<Rating> ratings = await _context.Ratings.ToListAsync();
            return Ok(ratings); //I guess it works, But not how i want it too. Will seek help after completing others
        }

        //GetRatingById?
        //GetRatingByRestaurantId?
    }
}
