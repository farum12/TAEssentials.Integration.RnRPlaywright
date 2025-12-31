Feature: Wishlist

    Background:
        Given User navigates to the application
        And API: New Standard User is exists in the system
        And API: New Book is exists in the system
        And User is logged in as Standard User
    
    Scenario: Standard User can add a book to wishlist
        When User clicks Products Nav Button in order to navigate to Products Page
        And User searches New Book by its title
        And User clicks on New Book View Details button
        And User clicks on Add to Wishlist button of the Book Details Page
        And User clicks on Wishlist Nav Button in order to navigate to Wishlist Page
        Then the New Book should be displayed in the Wishlist Page

    Scenario: Standard User can remove a book from wishlist
        When User clicks Products Nav Button in order to navigate to Products Page
        And User searches New Book by its title
        And User clicks on New Book View Details button
        And User clicks on Add to Wishlist button of the Book Details Page
        And User clicks on Wishlist Nav Button in order to navigate to Wishlist Page
        And User removes the New Book from the Wishlist Page
        Then the New Book should not be displayed in the Wishlist Page anymore
        When User refreshes the Wishlist Page
        Then the New Book should not be displayed in the Wishlist Page anymore