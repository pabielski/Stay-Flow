import type { RoomType } from "../types/roomTypes";

const API_URL = import.meta.env.VITE_API_URL;

export async function getRoomTypes(hotelId: number): Promise<RoomType[]> {
  const response = await fetch(`${API_URL}/api/roomtypes?hotelId=${hotelId}`);

  if (!response.ok) {
    throw new Error(
      `Nie udało się pobrać typów pokoi (kod ${response.status}).`,
    );
  }

  return response.json();
}
