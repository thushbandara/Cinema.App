import type { Movie } from "~/utils/seats";

type SeatMapProps = {
  movie: Movie;
  takenSeats?: string[];
  currentSeats?: string[];
};

export default function SeatMap({ movie, takenSeats, currentSeats }: SeatMapProps) {
  if (!movie) return null;
  const rows = Array.from({ length: movie.rows }, (_, i) =>
    String.fromCharCode(65 + i)
  );

  return (
    <div style={{ fontFamily: "monospace", marginTop: 20 }}>
      {/* screen */}
      <div style={{ textAlign: "center", fontWeight: "bold", marginBottom: 10 }}>
        SCREEN
      </div>

      {/* rows */}
      {rows.slice().reverse().map((row, rowIndex) => (
        <div
          key={row}
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            marginBottom: 6
          }}
        >
          {/* row label on the left */}
          <span style={{ width: 20, marginRight: 10, fontWeight: "bold" }}>
            {String.fromCharCode(65 + rowIndex)} {/* A=65 in ASCII */}
          </span>

          {/* seats */}
          {Array.from({ length: movie.seatsPerRow }, (_, i) => {
            const col = i + 1;
            const seatId = `${String.fromCharCode(65 + rowIndex)}${col}`;

            let bg = "#e0e0e0"; // free
            if (takenSeats?.includes(seatId)) bg = "#ff5252"; // booked
            if (currentSeats?.includes(seatId)) bg = "#4caf50"; // selected

            return (
              <div
                key={seatId}
                style={{
                  width: 28,
                  height: 28,
                  margin: 3,
                  borderRadius: 4,
                  backgroundColor: bg,
                  display: "flex",
                  alignItems: "center",
                  justifyContent: "center",
                  fontSize: 12,
                  color: "#fff",
                  cursor: "pointer"
                }}
                title={seatId}
              >
                {col}
              </div>
            );
          })}
        </div>
      ))}

      {/* column numbers under */}
      <div style={{ textAlign: "center", marginTop: 10 }}>
        <span style={{ width: 30, display: "inline-block" }} />
        {Array.from({ length: movie.seatsPerRow }, (_, i) => (
          <span
            key={i}
            style={{
              display: "inline-block",
              width: 28,
              margin: 3,
              fontSize: 12
            }}
          >
            {i + 1}
          </span>
        ))}
      </div>
    </div>

  );
}
