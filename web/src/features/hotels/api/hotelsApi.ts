import type {
  CreateHotelRequest,
  UpdateHotelRequest,
  Hotel,
} from "../types/hotel";
const API_URL = import.meta.env.VITE_API_URL;
export const hotelKeys = {
  all: ["hotels"] as const,
};

export async function getHotels(): Promise<Hotel[]> {
  const response = await fetch(`${API_URL}/api/hotels`);

  if (!response.ok) {
    throw new Error(await readErrorMessage(response));
  }

  return response.json();
}
export async function createHotel(request: CreateHotelRequest): Promise<Hotel> {
  const response = await fetch(`${API_URL}/api/hotels`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    throw new Error(await readErrorMessage(response));
  }

  return response.json();
}

async function readErrorMessage(response: Response): Promise<string> {
  try {
    const body = await response.json();
    if (body.detail) {
      return body.detail;
    }

    if (body.message) {
      return body.message;
    }

    if (body.errors) {
      return Object.values(body.errors).flat().join(" ");
    }
  } catch {
    // silent
  }

  return `Wystąpił błąd (kod ${response.status}).`;
}

export async function updateHotel(
  id: number,
  request: UpdateHotelRequest,
): Promise<void> {
  const response = await fetch(`${API_URL}/api/hotels/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    throw new Error(await readErrorMessage(response));
  }
}
export async function deactivateHotel(id: number): Promise<void> {
  const response = await fetch(`${API_URL}/api/hotels/${id}`, {
    method: "DELETE",
  });

  if (!response.ok) {
    throw new Error(await readErrorMessage(response));
  }
}
