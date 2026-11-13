import { StatusBar } from "expo-status-bar";
import { StyleSheet, Text } from "react-native";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { SafeAreaProvider, SafeAreaView } from "react-native-safe-area-context";
import { HotelsList } from "./src/features/hotels/components/HotelList";

const queryClient = new QueryClient();

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <SafeAreaProvider>
        <SafeAreaView style={styles.container}>
          <Text style={styles.title}>Hotele</Text>
          <HotelsList />
          <StatusBar style="dark" />
        </SafeAreaView>
      </SafeAreaProvider>
    </QueryClientProvider>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: "#f6f7f5" },
  title: {
    fontSize: 28,
    fontWeight: "700",
    color: "#24476b",
    paddingHorizontal: 16,
    paddingTop: 8,
  },
});
