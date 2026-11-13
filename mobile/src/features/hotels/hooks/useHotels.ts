import { useQuery } from "@tanstack/react-query";
import { getHotels, hotelKeys } from "../api/hotelsApi";

export function useHotels() {
  const query = useQuery({
    queryKey: hotelKeys.all,
    queryFn: getHotels,
  });

  return {
    hotels: query.data ?? [],
    isLoading: query.isPending,
    error: query.error?.message ?? null,
    isRefreshing: query.isRefetching,
    refresh: query.refetch,
  };
}
