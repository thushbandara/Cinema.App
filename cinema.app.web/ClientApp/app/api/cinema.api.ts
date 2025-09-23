import axios from "axios"
import type { AddBooking, Booking, DefineMovie, Movie, SearchBooking } from "../utils/seats";
const API_BASE_URL = "https://localhost:5001/api";


export async function createBooking(booking: AddBooking) {
  try {
    const res = await axios.post(`${API_BASE_URL}/cinema/book`, { ...booking });
    return res.data as Booking
  } catch (error) {
    throw error;
  }
}

export async function getAllBookings() {
  try {
    const res = await axios.get(`${API_BASE_URL}/get_all_bookings`);
    return res.data as Booking[]
  } catch (error) {
    throw error;
  }
}

export async function searchBookingByReference(ref: string) {
  try {
    const res = await axios.get(`${API_BASE_URL}/cinema/search/${ref}`);
    return res.data as SearchBooking
  } catch (error) {
    throw error;
  }
}

export async function defineCinema(movie: DefineMovie) {
  try {
    const res = await axios.post(`${API_BASE_URL}/cinema/define`, movie);
    return res.data as Movie
  } catch (error) {
    throw error;
  }
}


export async function loadCinema(movie: string) {
  try {
    const res = await axios.get(`${API_BASE_URL}/cinema/${movie}`);
    return res.data as Movie
  } catch (error) {
    throw error;
  }
}

