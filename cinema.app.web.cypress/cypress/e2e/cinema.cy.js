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
            .and("have.text", "GIC Cinemas")
    })


    it("should display the main heading", () => {
        cy.visit('http://localhost:5173/');
        cy.wait(1000);
        cy.get('[data-testid="seat-1-4"]', { timeout: 10000 }).click();
    })
});

