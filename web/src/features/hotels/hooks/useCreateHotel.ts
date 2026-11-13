import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createHotel, hotelKeys } from "../api/hotelsApi";

export function useCreateHotel() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createHotel,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: hotelKeys.all });
    },
  });
}
