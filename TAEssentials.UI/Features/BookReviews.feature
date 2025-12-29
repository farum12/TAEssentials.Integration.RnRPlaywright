Feature: Book Reviews

    Background:
        Given User navigates to the application
        And API: New Standard User is exists in the system
        #And API: New Book is created in the system
        And User is logged in as Standard User
    
    Scenario: Standard User can add a book review
        When User navigates to Main Page
        #And User clicks on "1984" book View Details button
        #And User adds a book review with rating "4" and comment "Great book!"
        #Then The book review should be successfully submitted