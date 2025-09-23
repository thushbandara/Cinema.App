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

    it("should display the buttons ", () => {
        cy.visit('http://localhost:5173/');
        cy.get("#btn-define")
            .should("exist")
            .and("be.visible")
            .and("have.text", "Define Movie");

        cy.get("#btn-book")
            .should("exist")
            .and("be.visible")
            .and("have.text", "Start Booking");

        cy.get("#btn-check")
            .should("exist")
            .and("be.visible")
            .and("have.text", "Check Booking");
    });
    
    it("should load the DefineMovie component when clicking Define Movie button", () => {
        cy.visit("http://localhost:5173/");

        cy.contains("Title").should("not.exist");
        cy.get("#btn-define").click();

        // cy.contains("Define Movie").should("be.visible");
        // cy.get("#txt-title").should("exist");
        // cy.get("#txt-rows").should("exist");
        // cy.get("#txt-seats-per-row").should("exist");
        // cy.contains("Save").should("exist");
    });

});
