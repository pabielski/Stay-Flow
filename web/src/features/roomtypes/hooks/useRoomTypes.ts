import { useQuery } from "@tanstack/react-query";
import { roomTypesKeys } from "../types/roomTypes";
import { getRoomTypes } from "../api/roomTypesApi";
export function useRoomTypes(hotelId: number) {
  const query = useQuery({
    queryKey: [...roomTypesKeys.all, hotelId],
    queryFn: () => getRoomTypes(hotelId),
  });
  const roomTypes = query.data ?? [];
  return {
    roomTypes,
    isLoading: query.isPending,
    error: query.error?.message ?? null,
  };
}
