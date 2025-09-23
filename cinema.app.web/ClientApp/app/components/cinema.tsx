import { useState } from "react"
import DefineMovie from "./define.movie";
import BookingFlow from "./booking.flow";
import CheckBooking from "./check.booking";
import type { Movie } from "~/utils/seats";

export default function Cinema() {
  const [activeTab, setActiveTab] = useState("");
  const [message, setMessage] = useState("");
  const [movie, setMovie] = useState({} as Movie);

  return (
    <div style={{ padding: 20 }}>
      <h1 id="cinema-title">GIC Cinemas</h1>
      <div style={{ marginBottom: 20 }}>
        {message && <div className="alert alert-info mt-3">{message}</div>}
        <button
          id="btn-define"
          className="btn btn-success m-2"
          onClick={() => setActiveTab("define")}>Define Movie</button>
        <button
          id="btn-book"
          className="btn btn-success m-2"
          disabled={!movie?.id}
          onClick={() => setActiveTab("book")}>Start Booking</button>
        <button
          id="btn-check"
           disabled={!movie?.id}
          className="btn btn-success m-2"
          onClick={() => setActiveTab("check")}>Check Booking</button>
      </div>
      {activeTab === "define" && <DefineMovie
        setMovie={setMovie}
        setMessage={setMessage}
        movie={movie}
        message={message} />}

      {activeTab === "book" && movie && <BookingFlow
        setMessage={setMessage}
        movie={movie} />}

      {activeTab === "check" && movie && <CheckBooking
        movie={movie}
        setMessage={setMessage} />}
    </div>
  )
}
