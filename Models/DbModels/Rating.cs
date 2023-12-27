namespace WONDERLUST_PROJECT_APIs.Models.DbModels
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int PackageId { get; set; }
        public string UserId { get; set; }
        public int RatingStar { get; set; }
        public string Description { get; set; }

        public virtual Package Package { get; set; }
        public virtual User User { get; set; }
    }
}
