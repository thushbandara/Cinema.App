export type Seat = {
    row: number
    column: number
    isTaken: boolean
    label: string
    isSelected: boolean
}

export type Booking = {
    bookingReference: string
    seats: Seat[]
}

export function generateSeats(
    rows: number,
    seatsPerRow: number,
    bookings: Booking[] = []
): Seat[] {
    const seats: Seat[] = []

    for (let r = 0; r < rows; r++) {
        for (let c = 0; c < seatsPerRow; c++) {
            const label = `${String.fromCharCode(65 + r)}${String(c + 1)}`
            let bookingReference: string | undefined = undefined
            let isTaken = false

            for (const b of bookings) {
                if (b.seats.some(s => s.row === r && s.column === c)) {
                    bookingReference = b.bookingReference
                    isTaken = true
                    break
                }
            }
            
            seats.push({
                row: r,
                column: c,
                isTaken,
                label,
                isSelected: false
            })
        }
    }
    return seats
}
