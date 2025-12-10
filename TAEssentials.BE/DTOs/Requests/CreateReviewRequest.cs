namespace TAEssentials.BE.DTOs.Requests;

public class CreateReviewRequest
{
    public int Rating { get; set; }
    public string? ReviewText { get; set; }
}
