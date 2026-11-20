import { useMutation, useQueryClient } from "@tanstack/react-query";
import { hotelKeys, updateHotel } from "../api/hotelsApi";
import type { UpdateHotelRequest } from "../types/hotel";

type UpdateHotelVariables = {
  id: number;
  request: UpdateHotelRequest;
};

export function useUpdateHotel() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, request }: UpdateHotelVariables) =>
      updateHotel(id, request),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: hotelKeys.all });
    },
  });
}
