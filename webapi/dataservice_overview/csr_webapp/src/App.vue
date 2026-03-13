<script setup>
import neodeexLogo from '@/assets/neodeex_logo.png';
import { ref, onMounted, computed, watch, onUnmounted } from 'vue';

const orders = ref([]);
const isLoading = ref(false);

const query = async () => {
  isLoading.value = true;
  // 응답이 너무 빨라도 최소 300ms 동안 로딩 상태를 유지하여 깜박임(flickering)을 방지합니다.
  const minLoadingPromise = new Promise(resolve => setTimeout(resolve, 300));
  try {
    const dataRequest = {
      queryId: 'orders.get_all_orders_with_details'
    }
    const [httpResponse, _] = await Promise.all([
      fetch("/api/dataservice/executedataset", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(dataRequest)
      }),
      minLoadingPromise
    ]);
    const dataResponse = await httpResponse.json();
    orders.value = dataResponse.dataSet.Table;
  } catch (error) {
    console.error('Error fetching data:', error);
    orders.value = [];
  } finally {
    isLoading.value = false;
  }
};

</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <div class="container">
      <span class="navbar-brand d-flex align-items-center" href="#">
        <img :src="neodeexLogo" alt="Neodeex Logo" height="24" class="me-2">
        JavaScript Client Sample - Fox Data Service
      </span>
    </div>
  </nav>

  <div class="container mt-2">
    <div>
      <div class="alert alert-info" role="alert">
        이 예제는 Vue 프레임워크를 사용하는 웹 애플리케이션으로, Fox Data Service를 호출하고 결과를 표시하는 기능을 보여줍니다.
        "Query" 버튼을 클릭하면 데이터베이스에서 주문 데이터를 가져와 테이블에 표시합니다.
        로딩 중에는 스피너가 나타나며, 결과가 없을 경우 사용자에게 알리는 메시지가 표시됩니다.
      </div>
    </div>
    <div class="col-md-auto">
      <button class="btn btn-primary query-btn" type="submit" @click="query" :disabled="isLoading">
        <span v-if="isLoading" class="spinner-border spinner-border-sm me-1" role="status" aria-hidden="true"></span>
        Get All Orders with Details
      </button>
    </div>
    <div class="card">
      <div class="card-header">
        Query Results
        <span v-if="orders.length > 0" class="badge bg-success ms-2">
          {{ orders.length }} rows
        </span>
      </div>
      <div class="card-body">
        <div class="table-responsive table-container">
          <table class="table table-striped table-hover table-sm table-fixed-layout">
            <thead>
              <tr>
                <th>Order Id</th>
                <th>Customer Id</th>
                <th>Order Date</th>
                <th>Product Id</th>
                <th>Quantity</th>
                <th>Unit Price</th>
              </tr>
            </thead>
            <tbody>
              <tr v-if="isLoading">
                <td colspan="6" class="text-center p-5">
                  <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Loading...</span>
                  </div>
                </td>
              </tr>
              <tr v-else-if="!isLoading && orders.length === 0">
                <td colspan="6" class="text-center p-5">
                  <div class="empty-results-container">
                    <svg class="empty-results-icon" xmlns="http://www.w3.org/2000/svg" width="80" height="80" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1" stroke-linecap="round" stroke-linejoin="round">
                      <path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"></path>
                      <polyline points="14 2 14 8 20 8"></polyline>
                      <circle cx="11.5" cy="14.5" r="2.5"></circle>
                      <path d="M13.25 16.25L15 18"></path>
                    </svg>
                    <p class="mt-3 mb-1 fs-5 text-muted">No Results Found</p>
                  </div>
                </td>
              </tr>
              <tr v-else v-for="item in orders" :key="item.infoId" @click="showDetail(item)">
                <td>{{ item.order_id }}</td>
                <td>{{ item.customer_id }}</td>
                <td>{{ item.order_date }}</td>
                <td>{{ item.product_id }}</td>
                <td>{{ item.quantity }}</td>
                <td>{{ item.unit_price }}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.navbar-brand {
  /* 네비게이션 바의 제목 폰트를 키웁니다. */
  font-size: 1.5rem;
}

button.query-btn {
  /* Query 버튼의 여백을 조정합니다. */
  margin-bottom: 1rem;
}
</style>
