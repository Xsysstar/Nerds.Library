# Nerds.Library

This web service implements a Library API as specified by [library-api.pdf](library-api.pdf). This is a programming test.

## Requirements analysis

Having read the specification, I am making the following modifications:

- simplify or postpone the requirements that require 'authentication'. I do that because
  - doing authentication and authorization is a complex topic I need to investigate further.
- simplify the requirements that require 'e-mailing' so that the *data* that should be e-mailed about can be queried via the API, but no actual e-mail is sent. I do that because
  - I reckon this data is useful in other scenarios as well (e.g., push notifications, web dashboards etc.).
  - having a background service running continuously (and guaranteed to do so properly) is complexity I need to investigate.
  - sending periodic e-mails could be done (perhaps even better) by outside systems that just query the API.
- implement some of the 'reserve/return' (loan, unloan) requirements together, because
  - they are complementary or supporting in a test cycle for the other.
  - no viable product could exists that only implements just loaning without returning.

## Development planning

I intend to develop this assignment as follows:

- Model the domain
- Configure project
  - Set-up metadata
  - Enable static analyzers
  - Install Swagger tools
- Model the API
- Implement the domain and API.
- Implement SpecFlow to perform automated testing.
- Implement the API with a mock-up domain.
- Implement a storage back-end.
- Implement authentication and authorization.
- ...
