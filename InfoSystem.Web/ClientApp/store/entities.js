import axios from 'axios'
import Vue from 'vue'

export default {
  state: {
    entities: [],
    currentEntityDisplay: ''
  },
  mutations: {
    setEntities(state, payload) {
      state.entities = payload
    },
    setCurrentEntityDisplay(state, payload) {
      state.currentEntityDisplay = payload
    },
    addEntity(state, payload) {
      state.entities.push(payload)
    },
    deleteEntity(state, payload) {
      let index = state.entities.indexOf(state.entities.find(x => x.id == payload))
      state.entities.splice(index, 1)
    }
  },
  actions: {
    async getCurrentEntityDisplay({ commit, rootState }, payload) {
      let displayKey = await axios({
        method: 'get',
        url: `/api/Property/GetAttributeValue?typeName=${payload.typeName}`,
        headers: { Authorization: rootState.user.token }
      })
      let response = await axios({
        method: 'get',
        url: `/api/Property/GetByPropertyName?typeName=${payload.typeName}&entityId=${payload.entityId}&propertyName=${displayKey.data}`,
        headers: { Authorization: rootState.user.token }
      })
      commit('setCurrentEntityDisplay', response.data.value)
    },
    async getEntities({ commit, rootState }, payload) {
      let response = await axios({
        method: 'get',
        url: `/api/Entity/GetByTypeName?typeName=${payload}`,
        headers: { Authorization: rootState.user.token }
      })
      setTimeout(() => {
        commit('setEntities', response.data)
      }, 0)
    },
    async addEntity({ commit, rootState }, payload) {
      commit('setLoading', true)
      let response = await axios({
        method: 'post',
        url: `/api/Entity/Add?typeName=${payload.typeName}&requiredAttributeValue=${payload.requiredAttributeValue}`,
        headers: { Authorization: rootState.user.token }
      })
      commit('addEntity', response.data)
      commit('setLoading', false)
    },
    async deleteEntity({ commit, rootState }, payload) {
      commit('setLoading', true)
      await axios({ method: 'delete', url: `/api/Entity/Delete?id=${payload}`, headers: { Authorization: rootState.user.token } })
      commit('deleteEntity', payload)
      commit('setLoading', false)
    }
  }
}
