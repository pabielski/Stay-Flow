import { useQuery } from "@tanstack/react-query";
import { getHotels, hotelKeys } from "../api/hotelsApi";

type UseHotelOptions = {
  includeInactive: boolean;
};
export function useHotels({ includeInactive }: UseHotelOptions) {
  const query = useQuery({
    queryKey: hotelKeys.all,
    queryFn: getHotels,
  });

  const allHotels = query.data ?? [];
  const hotels = includeInactive
    ? allHotels
    : allHotels.filter((hotel) => hotel.isActive);

  return {
    hotels,
    isLoading: query.isPending,
    error: query.error?.message ?? null,
  };
}
