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
      <div style={{ textAlign: "center", fontWeight: "bold", marginBottom: 20 }}>
        SCREEN
      </div>
      <div
        style={{
          textAlign: "center",
          borderTop: "4px solid #333",
          marginBottom: 30,
          width: movie.seatsPerRow * 24,
          margin: "0 auto"
        }}
      />

      {rows.slice().reverse().map(row => (
        <div
          key={row}
          style={{ display: "flex", alignItems: "center", justifyContent: "center", marginBottom: 6 }}
        >
          {Array.from({ length: movie.seatsPerRow }, (_, i) => {
            const col = i + 1;
            const seatId = `${row}${col}`;

            let bg = "#e0e0e0"; 
            if (takenSeats?.includes(seatId)) bg = "#ff5252"; 
            if (currentSeats?.includes(seatId)) bg = "#4caf50"; 

            return (
              <div
                key={seatId}
                style={{
                  width: 30,
                  height: 30,
                  margin: 2,
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

      <div style={{ textAlign: "center", marginTop: 10 }}>
        {Array.from({ length: movie.seatsPerRow }, (_, i) => (
          <span
            key={i}
            style={{ display: "inline-block", width: 22, margin: 2, fontSize: 12 }}
          >
            {i + 1}
          </span>
        ))}
      </div>
    </div>

  );
}
