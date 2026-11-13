import type { Hotel } from "../types/hotel";

const API_URL = process.env.EXPO_PUBLIC_API_URL;

export const hotelKeys = {
  all: ["hotels"] as const,
};

export async function getHotels(): Promise<Hotel[]> {
  const response = await fetch(`${API_URL}/api/hotels`);

  if (!response.ok) {
    throw new Error(`Nie udało się pobrać hoteli (kod ${response.status}).`);
  }

  return response.json();
}
