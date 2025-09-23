import { useState, useEffect } from "react";
import SeatMap from "./seat.map";
import type { Booking, Movie } from "~/utils/seats";
import { createBooking, loadCinema } from "~/api/cinema.api";

interface BookingFlowProps {
    movie: Movie;
    setMessage: (msg: string) => void;
}

export default function BookingFlow({
    movie,
    setMessage
}: BookingFlowProps) {
    const [tickets, setTickets] = useState("");
    const [seatCode, setSeatCode] = useState("");
    const [booking, setBooking] = useState({} as Booking);
    const [currentMovie, setCurrentMovie] = useState(movie);

    function loadMovie() {
        loadCinema(movie.id).then((movie) => {
            setCurrentMovie(movie);
        }).catch((error) => {
            if (error.code === 'ERR_NETWORK') {
                setMessage(`Backend server is not running. Please start the server and try again.`);
                return;
            }
            setMessage(`Error: ${error.response?.data.Errors[0]}` || `Error occurred.`);
        })
    }

    function book() {
        createBooking({
            cinemaId: movie.id,
            tickets: parseInt(tickets),
            startRow: seatCode ? seatCode.charAt(0) : null,
            startCol: seatCode ? parseInt(seatCode.slice(1)) : null
        }).then((data) => {
            setMessage(`Successfully reserved ${tickets} tickets. Booking Id: ${data.bookingReference}`);
            setBooking(data);
            loadMovie();
        }).catch((error) => {
            if (error.code === 'ERR_NETWORK') {
                setMessage(`Backend server is not running. Please start the server and try again.`);
                return;
            }
            if (error.response?.data?.errors) {
                const errors = Object.values(error.response.data.errors).flat();
                setMessage(`Error: ${errors[0]}`);
            } else {
                setMessage(`Error occurred.`);
            }
        });
    }

    useEffect(() => {
        loadMovie();
    }, []);

    return (
        <div>
            <h3>Book Tickets for {currentMovie?.title}</h3>
            <input type="number"
                placeholder="Tickets"
                id="txt-ticket"
                className="form-control w-auto mb-2"
                value={tickets} onChange={e => setTickets(e.target.value)} />
            <input type="text"
                placeholder="Seat code (optional, e.g. B3)"
                id="txt-code"
                className="form-control w-auto mb-2"
                value={seatCode} onChange={e => setSeatCode(e.target.value.toUpperCase())} />
            <button
                id="btn-check"
                className="btn btn-success m-2"
                onClick={book}>Reserve</button>

            {booking && (
                <>
                    <h4>Booking Ref: {booking.bookingReference}</h4>
                    <SeatMap movie={currentMovie} takenSeats={currentMovie.takenSeats} currentSeats={booking.seats} />
                </>
            )}

            {!booking && currentMovie && (
                <SeatMap movie={currentMovie} takenSeats={currentMovie.takenSeats} />
            )}
        </div>
    );
}
