import { useState } from "react";
import { searchBookingByReference } from "../api/cinema.api"
import type { Booking, Movie, SearchBooking } from "~/utils/seats";
import SeatMap from "./seat.map";

interface CheckBookingProps {
    movie: Movie;
    setMessage: (msg: string) => void;
}

export default function CheckBooking({
    movie,
    setMessage
}: CheckBookingProps) {

    const [bookingId, setBookingId] = useState("");
    const [result, setResult] = useState<SearchBooking>();

    function handleCheck() {
        const res = searchBookingByReference(bookingId)
            .then(response => {
                setResult(response);
                return;
            });

        setMessage("Booking not found");
    }

    return (
        <div>
            <h3>Search Booking</h3>
            <input placeholder="Booking Ref"
                id="txt-ticket"
                className="form-control w-auto mb-2"
                value={bookingId}
                onChange={e => setBookingId(e.target.value)} />
            <button className="btn btn-success m-2" onClick={handleCheck}>Search</button>

            <SeatMap
                movie={movie}
                takenSeats={result?.takenSeats}
                currentSeats={result?.selectedSeats}
            />
        </div>
    );
}
