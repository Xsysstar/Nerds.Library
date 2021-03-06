# Nerds.Library

This web service implements a Library API as specified by [library-api.pdf](library-api.pdf). This is a programming test.

## Requirements analysis

Having read the specification, I am making the following modifications:

### Authentication

I'd simplify or postpone the requirements that require 'authentication'. I do that because doing authentication and authorization is a complex topic I need to investigate further.

### E-mailing

I'd simplify the requirements that require 'e-mailing' so that the *data* that should be e-mailed about can be queried via the API, but no actual e-mail is sent. I do that because:

- I reckon this data is useful in other scenarios as well (e.g., push notifications, web dashboards etc.).
- having a background service running continuously (and guaranteed to do so properly) is complexity I need to investigate.
- sending periodic e-mails could be done (perhaps even better) by outside systems that just query the API.

### Reserving and returning

I'd implement some of the 'reserve/return' (loan, unloan) requirements together, because
  - they are complementary or supporting in a test cycle for the other.
  - no viable product could exists that only implements just loaning without returning.

### Limiting scope

I'm not doing anything about:

- library business policies (late fees);
- content administration (managing the books in the library itself)
- warehouse management (in particular: substituting one physical book copy for another)

There's no explicit requires to implement these features and there's a world of possibilities there.

There's more things to investigate in the world of libraries, like the [Dewey Decimal Classification](https://en.wikipedia.org/wiki/Dewey_Decimal_Classification) and e-books. Not doing anything with that

## Development planning

I intend to develop this assignment as follows:

- Model the domain
- Configure project
  - Set-up metadata
  - Enable static analyzers ([FxCop](https://www.nuget.org/packages/Microsoft.CodeAnalysis.FxCopAnalyzers))
  - Install Swagger tools ([SwashBuckle](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio)
- Implement (mock-up) domain.
- Model and implement the API for the domain queries (details, lists, sorting, filtering)
- Model and implement the API for the domain commands/mutations (reservations, returns, rating)
- Implement SpecFlow to perform automated testing.
- Implement a storage back-end.
- Implement authentication and authorization.
- ...

## Domain analysis

See the following class diagram:

![Class diagram](library-api.png)

### Book instances versus types

Distinguishing between book instances and book types is harder than it looks.

One could argue every physical book instance can be perfectly tracked in the world, but that would require a unique barcode or something. I'm assuming we'll do that.

A single 'book type' is also hard, given that (effectively) the same book could have different ISBNs or slightly different books could still have the same ISBN (e.g., reprints). Therefore, we're using book 'templates' instead of full-on 'types', just a way to prevent unnecessary data duplication, but we're not forcing books into a strong 'type classification system'.

### ISBN

ISBN exists in two formats: ISBN-10 or ISBN-13. The same books can have codes in both. I'll presume we can convert everything to the more modern ISBN-13 and we can forget about ISBN-10.

The same book in different languages or different forms (hardcover, softcover) will have different ISBNs, even though they are interchangeable for many purposes.

### Dimension classes

I'm splitting off attributes into separate classes (or "dimension") like a [star schema](https://en.wikipedia.org/wiki/Star_schema) because:

- Book metadata can get messy quickly. We may want to correct a whole slew of metadata at once.
- Having separate dimension tables can also have technical benefits for data marts and performance.
- Having separate classes may also allow those classes (e.g., `Publisher`, `Author`, `Genre`) to become first-class citizens in our system. (Indeed, sorting on 'Authors' that are deceased is such a case.)
- I also assume we might want to localize some captions to other languages (e.g, `Genre` and `Title`). I do not implement that but I have some ideas on how to do that.
- There may be multiples of many things (`Author`s, `Genre`s): parsing and splitting string properties would be wasteful.

If we had stuck with simple string attributes in the `BookTemplate` class (without dimension classes), we might choose to stick to the [Bibtex specification](https://en.wikibooks.org/wiki/LaTeX/Bibliography_Management).

### Ratings

The origin of ratings remains unspecified. I assume customers might submit a rating at some point (e.g., during the book 'returning' process). The system does not force this process and would currently allow anyone to add a rating at any time.

### BookBusiness

I introduced a class `BookBusiness` (one per `BookTemplate`) to manage the business-related and customer-oriented aspects around books. In particular `Reservation`s and `Review`s would have no reason for existing without `Customer`s, so I put them in a separate package.

## Implementation

### OData

I tried to simply apply the [OData](https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/supporting-odata-query-options) support in ASP.NET to publish a standardized API with support for filtering, ordering etc. It's [a bit more involved](https://devblogs.microsoft.com/odata/asp-net-core-odata-now-available/) that I expected, so I discarded the approach for now, merely taking some inspiration from it.

### Further improvements

- Implement pagination in the API.
- Implement Equals, GetHashCode, ToString (e.g., using [Fody](https://github.com/Fody/Fody))
- (If still there) Do not reuse domain classes in API layer.
- Performance. This is a prototype; it naive approach to querying and finding data is not scalable.

## Testing

This is a test run through the software (as delivered). It illustrates:

 - enumerating all/available books (requirement 1)
 - details for a specific book (requirement 2)
 - reserving a book (requirement 3)

### GET /api/Books
```
[
  {
    "id": "2e13a0a1-5688-4b18-9c3a-020609bd4738",
    "uniqueBarcode": "2e13a0a156884",
    "publicationDate": "2018-11-06T21:13:11.4705772+01:00",
    "isbn": "978-3-16-148410-9",
    "publisher": "Springer",
    "title": "The Art of War",
    "authors": [
      "J.K. Rowling",
      "Kevlin Henney",
      "Anonymous"
    ],
    "genres": [
      "Romance"
    ]
  },
  ...
]
```

### GET /api/Books/2e13a0a156884
```
{
  "id": "2e13a0a1-5688-4b18-9c3a-020609bd4738",
  "uniqueBarcode": "2e13a0a156884",
  "publicationDate": "2018-11-06T21:13:11.4705772+01:00",
  "isbn": "978-3-16-148410-9",
  "publisher": "Springer",
  "title": "The Art of War",
  "authors": [
    "Kevlin Henney",
    "Hans Kazan"
  ],
  "genres": [
    "Romance"
  ]
}
```


### GET /api/Customer
```
[
  {
    "id": "cab703f8-f948-4fb1-bb8b-89b055866651",
    "name": "Customer cab703f8-f948-4fb1-bb8b-89b055866651"
  },
  {
    "id": "2e4d4ffc-76c9-4b9d-8b5a-2e8447366d30",
    "name": "Customer 2e4d4ffc-76c9-4b9d-8b5a-2e8447366d30"
  }
]
```

### GET /api/Availability
```
[
  {
    "bookDetails": {
      "id": "2e13a0a1-5688-4b18-9c3a-020609bd4738",
      "uniqueBarcode": "2e13a0a156884",
      "publicationDate": "2018-11-06T21:13:11.4705772+01:00",
      "isbn": "978-3-16-148410-9",
      "publisher": "Springer",
      "title": "The Art of War",
      "authors": [
        "Kevlin Henney",
        "J.K. Rowling"
      ],
      "genres": [
        "Romance",
        "Documentary"
      ]
    },
    "isAvailable": true
  },
  ...
]
```
Notice `"isAvailable": true`.

### POST /api/Availability/reserve/2e13a0a156884?customerId=cab703f8-f948-4fb1-bb8b-89b055866651
```
{
  "bookDetails": {
    "id": "2e13a0a1-5688-4b18-9c3a-020609bd4738",
    "uniqueBarcode": "2e13a0a156884",
    "publicationDate": "2018-11-06T21:13:11.4705772+01:00",
    "isbn": "978-3-16-148410-9",
    "publisher": "Springer",
    "title": "The Art of War",
    "authors": [
      "J.K. Rowling",
      "Kevlin Henney",
      "Tolkien"
    ],
    "genres": [
      "Romance",
      "Fake news"
    ]
  },
  "isAvailable": false
}
```
Notice `"isAvailable": false`.

### GET /api/Availability
```
[
  {
    "bookDetails": {
      "id": "2e13a0a1-5688-4b18-9c3a-020609bd4738",
      "uniqueBarcode": "2e13a0a156884",
      "publicationDate": "2018-11-06T21:13:11.4705772+01:00",
      "isbn": "978-3-16-148410-9",
      "publisher": "Springer",
      "title": "The Art of War",
      "authors": [
        "Kevlin Henney",
        "J.K. Rowling",
        "Anonymous"
      ],
      "genres": [
        "Drama",
        "Action"
      ]
    },
    "isAvailable": false
  },
  ...
]
```
Notice `"isAvailable": false`.

## Evaluation

Small evaluation of the delivered product:

- Requirements 1 through 3 are mostly implemented
- Analysis for the other requirements and relevant domain entities is completed.
- Automated testing is not implemented. Some (temporary) debug assertions are included, though.

Some things I like in particular:

- The Dummy factory
- The domain analysis
- Using SwashBuckle to generate Swagger-specifications from documentation and code
- Using Static analyzers (although I barely did this time)

Some things I dislike:

- Non-functional requirements (like performance, scalability) were barely considered.
- I didn't follow my original plan to first implement the all 'query' requirements.
- My development iterations are not as 'agile' as intended; it's got hints of waterfall (although the project is small, of course).