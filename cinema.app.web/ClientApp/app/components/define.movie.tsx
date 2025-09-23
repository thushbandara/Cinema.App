import { useState } from "react";
import { defineCinema } from "../api/cinema.api"

type DefineMovieProps = {
    movie: any;
    setMovie: (movie: any) => void;
    message: string;
    setMessage: (msg: string) => void;
};

export default function DefineMovie({
    movie,
    setMovie,
    message,
    setMessage
}: DefineMovieProps) {
    const [title, setTitle] = useState("");
    const [rows, setRows] = useState("");
    const [seatsPerRow, setSeatsPerRow] = useState("");

    async function handleDefine() {
        if (!title || !rows || !seatsPerRow) {
            alert("Please fill all fields");
            return;
        }

        defineCinema({
            title,
            rows: parseInt(rows),
            seatsPerRow: parseInt(seatsPerRow)
        }).then(movie => {
            setMessage(`Movie ${title} defined with ${rows} rows and ${seatsPerRow} seats per row.`);
            setMovie(movie);
        }).catch(error => {
            if (error.code === 'ERR_NETWORK') {
                setMessage(`Backend server is not running. Please start the server and try again.`);
                return;
            }
            setMessage(`Error: ${error.response?.data.Errors[0]}` || `Error occurred.`);
        });
    }

    return (
        <div>
            <h3>Define Movie {movie && <span>- ({movie.title})</span>}</h3>
            {message && <div className="alert alert-info mt-3">{message}</div>}
            <input
                placeholder="Title"
                id="txt-title"
                className="form-control w-auto mb-2"
                value={title} onChange={e => setTitle(e.target.value)} />
            <input type="number"
                id="txt-rows"
                className="form-control w-auto mb-2"
                placeholder="Rows" value={rows} onChange={e => setRows(e.target.value)} />
            <input type="number" placeholder="Seats per row"
                id="txt-seats-per-row"
                className="form-control w-auto mb-2"
                value={seatsPerRow} onChange={e => setSeatsPerRow(e.target.value)} />

            <button className="btn btn-success m-2" onClick={handleDefine}>Save</button>
        </div>
    );
}
