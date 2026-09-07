# Add Product Reviews Feature

Implement a product review system allowing authenticated customers to rate and review products. Reviews will be displayed on a new Product Detail page, which will show individual reviews as well as aggregate stats (average rating and total count). Users will be able to edit or delete their own reviews. Admins will have the ability to view all reviews and delete inappropriate ones from the Admin dashboard.

## User Review Required

> [!WARNING]
> This plan introduces new database entities (`Review`). You will need to run `Add-Migration AddReviews` and `Update-Database` using the Package Manager Console (or `dotnet ef` CLI) after these changes are applied to update your SQL Server database.

## Proposed Changes

### Domain Layer (myshop.DAL / myshop.Entities)

We will introduce the `Review` entity and modify existing entities to support the relationships. 

#### [NEW] [Review.cs](file:///c:/Project_Shop/myshop.DAL/Models/Review.cs)
- Create a `Review` class implementing `ISoftDelete` and `IAuditable`.
- Include properties: `Id`, `ProductId`, `UserId`, `Rating` (1-5), `Comment`, `CreatedAt`, etc.

#### [NEW] [ReviewConfiguration.cs](file:///c:/Project_Shop/myshop.DAL/Configurations/ReviewConfiguration.cs)
- Implement `IEntityTypeConfiguration<Review>` to configure max length for `Comment`.
- Add a unique index on `ProductId` and `UserId` to ensure a user can only review a product once.
- Add a check constraint for `Rating` to be between 1 and 5.

#### [MODIFY] [Product.cs](file:///c:/Project_Shop/myshop.DAL/Models/Product.cs)
- Add `public List<Review> Reviews { get; set; }` to establish the 1-to-many relationship.

#### [MODIFY] [ApplicationUser.cs](file:///c:/Project_Shop/myshop.DAL/Models/ApplicationUser.cs)
- Add `public List<Review> Reviews { get; set; }`.

#### [MODIFY] [ApplicationDbContext.cs](file:///c:/Project_Shop/myshop.DAL/Data/ApplicationDbContext.cs)
- Add `public DbSet<Review> Reviews { get; set; }`.

---

### Data Access Layer (myshop.DAL)

We need to add a repository for the `Review` entity so the BLL can interact with it via the `UnitOfWork`.

#### [NEW] [IReviewRepo.cs](file:///c:/Project_Shop/myshop.DAL/IRepository/IReviewRepo.cs)
- Interface extending `IGenericRepo<Review>` with review-specific queries (e.g., fetching reviews by product ID including the User details).

#### [NEW] [ReviewRepo.cs](file:///c:/Project_Shop/myshop.DAL/Repository/ReviewRepo.cs)
- Implementation of `IReviewRepo`.

#### [MODIFY] [IUnitOfWork.cs](file:///c:/Project_Shop/myshop.DAL/Iconfiguration/IUnitOfWork.cs)
- Expose `IReviewRepo Reviews { get; }`.

#### [MODIFY] [UnitOfWork.cs](file:///c:/Project_Shop/myshop.DAL/Data/UnitOfWork.cs)
- Implement the `Reviews` property.

---

### Business Logic Layer (myshop.BLL)

We will create DTOs, AutoMapper profiles, and a Service to manage the business rules for reviews. 

#### [NEW] Review DTOs
- Create `myshop.BLL/DTOs/Review/ReviewDTO.cs` (for displaying a review).
- Create `myshop.BLL/DTOs/Review/CreateReviewDTO.cs` (for submitting a review).
- Create `myshop.BLL/DTOs/Review/EditReviewDTO.cs` (for updating a review).

#### [NEW] [ProductDetailsDTO.cs](file:///c:/Project_Shop/myshop.BLL/DTOs/Product/Customer/ProductDetailsDTO.cs)
- Create a DTO to aggregate the `DisplayProductDTO`, average rating, review count, and a list of `ReviewDTO`s.

#### [NEW] [ReviewProfile.cs](file:///c:/Project_Shop/myshop.BLL/Mapping/ReviewMapping/ReviewProfile.cs)
- Create AutoMapper configurations mapping `Review` to `ReviewDTO`, and `CreateReviewDTO`/`EditReviewDTO` to `Review`.

#### [NEW] [IReviewService.cs](file:///c:/Project_Shop/myshop.BLL/IServices/IReviewService.cs) & [ReviewService.cs](file:///c:/Project_Shop/myshop.BLL/Services/ReviewService.cs)
- Business logic for adding, editing, and deleting reviews.
- Include an Admin-specific method to fetch all reviews across all products to manage them.

#### [MODIFY] [IProductService.cs](file:///c:/Project_Shop/myshop.BLL/IServices/IProductService.cs) & [ProductService.cs](file:///c:/Project_Shop/myshop.BLL/Services/ProductService.cs)
- Add `Task<ProductDetailsDTO> GetProductDetailsForCustomerAsync(int productId)` to aggregate product info, its reviews, and average rating.

---

### Presentation Layer (myshop.Web)

We need to create the UI for the product detail page, Admin management pages, and endpoints to process review submissions.

#### [MODIFY] [Program.cs](file:///c:/Project_Shop/myshop.Web/Program.cs)
- Register `IReviewService`.
- Add `typeof(ReviewProfile)` to the `AddAutoMapper` configuration.

#### [NEW] [ReviewController.cs](file:///c:/Project_Shop/myshop.Web/Controllers/ReviewController.cs)
- Add endpoints for `AddReview`, `EditReview`, and `DeleteReview` (requires `[Authorize]`). These endpoints will redirect back to the product details page.

#### [NEW] [AdminReviewController.cs](file:///c:/Project_Shop/myshop.Web/Controllers/AdminReviewController.cs)
- Add an Admin controller with `[Authorize(Roles = "Admin")]` to manage reviews (e.g. `Index` to list them, `Delete` to remove inappropriate ones).

#### [MODIFY] [CustomerController.cs](file:///c:/Project_Shop/myshop.Web/Controllers/CustomerController.cs)
- Add `[HttpGet] public async Task<IActionResult> ProductDetails(int id)` to fetch and serve the product details view.

#### [MODIFY] [DisplayProducts.cshtml](file:///c:/Project_Shop/myshop.Web/Views/Customer/DisplayProducts.cshtml)
- Add a "View Details" button to the product cards linking to `/Customer/ProductDetails/{id}`.

#### [NEW] [ProductDetails.cshtml](file:///c:/Project_Shop/myshop.Web/Views/Customer/ProductDetails.cshtml)
- A new view showing the product information, average rating, and a list of reviews.
- Include a form to submit a rating (1-5 stars) and a written review for logged-in users who haven't reviewed yet.
- Display "Edit" and "Delete" buttons next to the review created by the currently logged-in user.

#### [NEW] [Index.cshtml](file:///c:/Project_Shop/myshop.Web/Views/AdminReview/Index.cshtml)
- Admin view displaying a DataTable of all reviews, allowing the admin to delete reviews.

#### [MODIFY] [_Layout.cshtml](file:///c:/Project_Shop/myshop.Web/Views/Shared/_Layout.cshtml)
- Add "Manage Reviews" to the Admin sidebar navigation.

## Verification Plan

### Manual Verification
1. Run the EF Migrations (`Add-Migration AddReviews` -> `Update-Database`).
2. Run the application (`F5`).
3. Log in as a customer. Navigate to the storefront and click "View Details" on a product.
4. Submit a review with a rating and comment. Verify it appears and the product's average rating updates.
5. Attempt to submit another review for the same product to ensure prevention logic works.
6. Edit and Delete the review as a customer.
7. Log in as an Admin. Navigate to "Manage Reviews" in the dashboard.
8. Verify all reviews are listed and can be successfully deleted by the admin.
