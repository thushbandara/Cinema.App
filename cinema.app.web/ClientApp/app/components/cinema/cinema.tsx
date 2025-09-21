import { useEffect, useState } from "react"
import { generateSeats, type Seat } from "../../utils/seats"
import { createBooking, getAllBookings, searchBookingByReference } from "../../api/cinema.api"

export default function Cinema() {
  const [seats, setSeats] = useState<Seat[]>([])
  const [message, setMessage] = useState("")
  const [searchId, setSearchId] = useState("")
  const [viewMode, setViewMode] = useState(false)
  const [highlighted, setHighlighted] = useState<Set<string>>(new Set())
  const rows = 8;
  const seatsPerRow = 10;

  useEffect(() => {
    getBookings();
  }, [])

  function toggleSeat(seat: Seat) {
    if (seat.isTaken || viewMode) {
      return;
    }

    setSeats(prev =>
      prev.map(s =>
        s.row === seat.row && s.column === seat.column
          ? { ...s, isSelected: !s.isSelected }
          : s
      )
    )
  }

  function confirmBooking() {
    const selectedSeats = seats.filter(s => s.isSelected);

    if (selectedSeats.length === 0) {
      setMessage("No seats selected!")
      return
    }

    submitBooking();
  }

  function getBookings() {
    getAllBookings().then(response => {
      const initial = generateSeats(rows, seatsPerRow, response);

      setSeats(initial);
    }).catch(error => {
      if (error.code === 'ERR_NETWORK') {
        setMessage(`Backend server is not running. Please start the server and try again.`);
      }
      setMessage(`Error occurred while retrieving bookings: ${error}`);
    });
  }

  function submitBooking() {
    const selected = seats.filter(s => s.isSelected);
    createBooking(selected).then(response => {

      setMessage(`Successfully reserved ${selected.length} Inception tickets. Booking Id: ${response}`);

      setSeats(prev =>
        prev.map(s =>
          s.isSelected
            ? {
              ...s, isSelected: false,
              isTaken: true,
              bookingReference: response
            } : s
        )
      )
    }).catch(error => {
      if (error.code === 'ERR_NETWORK') {
        setMessage(`Backend server is not running. Please start the server and try again.`);
      }
      setMessage(`Error occurred while creating bookings: ${error}`);
    });;

  }

  function searchBooking() {
    searchBookingByRef(searchId);
  }


  function searchBookingByRef(bookingRef: string) {
    if (!bookingRef) {
      setMessage("Please enter booking id.");
      setViewMode(true);
      return;
    }

    searchBookingByReference(bookingRef).then(response => {

      const booked = response.seats.map(s => `${s.row}-${s.column}`)
      setViewMode(true)
      setHighlighted(new Set(booked))
      setMessage(`Booking Id : ${searchId}`)

    }).catch(error => {
      if (error.code === 'ERR_NETWORK') {
        setMessage(`Backend server is not running. Please start the server and try again.`);
      }
      else if (error.response && error.response.status === 404) {
        setMessage("Booking not found.")
        setViewMode(true)
        setHighlighted(new Set())
        return
      }
    });
  }


  function clearSearch() {
    setViewMode(false);
    setSearchId("");
    setHighlighted(new Set());
    setMessage("");
  }

  return (
    <>
      <div className="container py-4">
        <h2 className="mb-3" id="cinema-title">GIC Cinemas</h2>

        {message && <div className="alert alert-info mt-3">{message}</div>}
        <div className="mb-3 d-flex gap-2">
          <input
            id="txt-search-booking"
            className="form-control w-auto"
            placeholder="Enter booking id"
            value={searchId}
            onChange={e => setSearchId(e.target.value)}
          />
          <button id="btn-search-booking" className="btn btn-primary" onClick={searchBooking}>
            Search Booking
          </button>
          {viewMode && (
            <button id="btn-cancel-booking" className="btn btn-secondary" onClick={clearSearch}>
              Cancel
            </button>
          )}
        </div>

        <div className="d-flex flex-column gap-2">
          {
            Array.from({ length: rows }).map((_, r) => (
              <div key={r} className="d-flex align-items-center gap-2">
                <strong>{String.fromCharCode(65 + r)}</strong>
                <div className="d-flex gap-2">
                  {
                    Array.from({ length: seatsPerRow }).map((_, c) => {
                      const seat = seats.find(s => s.row === r && s.column === c)
                      const key = `${r}-${c}`
                      const isHighlighted = highlighted.has(key)

                      let btnClass = "btn btn-sm "
                      if (isHighlighted) btnClass += "btn-danger"
                      else if (seat?.isTaken) btnClass += "btn-dark"
                      else if (seat?.isSelected) btnClass += "btn-success"
                      else btnClass += "btn-outline-secondary"

                      return (
                        <button
                          id={`btn-seat-${key}`}
                          data-testid={`seat-${key}`}
                          key={c}
                          className={btnClass}
                          style={{ width: "48px", height: "48px" }}
                          disabled={viewMode || seat?.isTaken}
                          onClick={() => toggleSeat(seat!)}
                        >
                          {seat?.isTaken || isHighlighted ? "X" : seat?.label}
                        </button>
                      )
                    })}
                </div>
              </div>
            ))}

          <div className="d-flex ms-3 gap-2">
            {Array.from({ length: seatsPerRow }).map((_, c) => (
              <span
                key={c}
                style={{ width: "48px", textAlign: "center", fontWeight: "bold" }}
              >
                {c + 1}
              </span>
            ))}
          </div>
        </div>

        <div className="mt-3">
          <button
            id="btn-confirm-booking"
            className="btn btn-success"
            onClick={confirmBooking}
            disabled={viewMode}
          >
            Confirm Booking
          </button>
        </div>
      </div>
    </>
  )
}
