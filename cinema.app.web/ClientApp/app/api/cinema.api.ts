import axios from "axios"
import type { Booking, Seat } from "../utils/seats";
const API_BASE_URL = "https://localhost:5001/api";


export async function createBooking(seats: Seat[]) {
  try {
    const res = await axios.post(`${API_BASE_URL}/create_booking`, { seats });
    return res.data as string
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
    const res = await axios.get(`${API_BASE_URL}/search_booking/${ref}`);
    return res.data as Booking
  } catch (error) {
    throw error;
  }
}

