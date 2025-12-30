Feature: Book Reviews

    Background:
        Given User navigates to the application
        And API: New Standard User is exists in the system
        And API: New Book is exists in the system
        And User is logged in as Standard User
    
    Scenario: Standard User can add a book review
        When User clicks Products Nav Button in order to navigate to Products Page
        And User searches New Book by its title
        And User clicks on New Book View Details button
        And User clicks on Write a Review button
        And User adds a book review with rating "4" and some comment
        Then The book review should be successfully submitted