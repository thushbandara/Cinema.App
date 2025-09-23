// export type Seat = {
//     row: number
//     column: number
//     isTaken: boolean
//     label: string
//     isSelected: boolean
// }

export type Booking = {
    movieId: string
    bookingReference: string
    seats: string[]
}

export type SearchBooking = {
    movieId: string
    bookingReference: string
    selectedSeats : string[]
    takenSeats : string[]
}

export type AddBooking = {
      cinemaId: string    
      tickets :number;
      startRow :string | null;
      startCol :number | null;
}



export type DefineMovie = {
    rows: number
    seatsPerRow: number
    title: string
}

export type Movie = {
    id: string;
    title: string;
    rows: number;
    seatsPerRow: number;
    takenSeats?: any[];
}



// export function generateSeats(
//     rows: number,
//     seatsPerRow: number,
//     bookings: Booking[] = []
// ): Seat[] {
//     const seats: Seat[] = []

//     for (let r = 0; r < rows; r++) {
//         for (let c = 0; c < seatsPerRow; c++) {
//             const label = `${String.fromCharCode(65 + r)}${String(c + 1)}`
//             let bookingReference: string | undefined = undefined
//             let isTaken = false

//             for (const b of bookings) {
//                 if (b.seats.some(s => s.row === r && s.column === c)) {
//                     bookingReference = b.bookingReference
//                     isTaken = true
//                     break
//                 }
//             }
            
//             seats.push({
//                 row: r,
//                 column: c,
//                 isTaken,
//                 label,
//                 isSelected: false
//             })
//         }
//     }
//     return seats
// }
