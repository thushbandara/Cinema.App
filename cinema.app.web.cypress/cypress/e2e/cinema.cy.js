// file.js
// Placeholder for Cypress end-to-end tests for Cinema.App web application

describe('Cinema.App Web E2E', () => {
    it('should load the homepage', () => {
        cy.visit('http://localhost:5173/');
    });

    it("should display the main heading", () => {
        cy.visit('http://localhost:5173/');
        cy.get("#cinema-title")
            .should("exist")
            .and("be.visible")
            .and("have.text", "GIC Cinemas");
    });

    it('should allow selecting a seat', () => {
        cy.visit('http://localhost:5173/');
        cy.wait(1000);
        cy.get('[data-testid="seat-1-4"]', { timeout: 10000 }).click();
        cy.get('[data-testid="seat-1-4"].selected').should('exist');
    });

    it('should show available movies', () => {
        cy.visit('http://localhost:5173/');
        cy.get('[data-testid="movie-list"]').should('exist').and('be.visible');
        cy.get('[data-testid^="movie-item-"]').should('have.length.greaterThan', 0);
    });

    it('should display showtimes for a selected movie', () => {
        cy.visit('http://localhost:5173/');
        cy.get('[data-testid^="movie-item-"]').first().click();
        cy.get('[data-testid="showtime-list"]').should('exist').and('be.visible');
    });

    it('should proceed to checkout after seat selection', () => {
        cy.visit('http://localhost:5173/');
        cy.get('[data-testid^="movie-item-"]').first().click();
        cy.get('[data-testid^="showtime-item-"]').first().click();
        cy.get('[data-testid="seat-1-4"]').click();
        cy.get('[data-testid="proceed-checkout"]').click();
        cy.url().should('include', '/checkout');
    });
});
