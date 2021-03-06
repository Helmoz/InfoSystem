<template>
  <div>
    <v-card class="elevation-1 pa-3">
      <v-card-text>
        <div class="layout column align-center">
          <img src="../../assets/logo.png" alt="Vue Material Admin" width="120" height="120" />
          <h1 class="flex my-4 primary--text">InfoSystem | {{ value ? 'SignUp' : 'LogIn' }}</h1>
        </div>
        <v-form>
          <username-field :v="$v" :username.sync="username" :submit="submit" />
          <password-field :v="$v" :password.sync="password" :submit="submit" />
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn large color="primary" :disabled="$v.$invalid" block @click="submit">{{ buttonText }}</v-btn>
      </v-card-actions>
    </v-card>
    <v-card flat class="mt-3 card_switch elevation-2">
      {{ switchText }}
      <a @click="$emit('update:isSignUp', !value)">{{ switchLink }}</a>
    </v-card>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'nuxt-property-decorator'

import { Validation } from 'vuelidate'
import { required, minLength } from 'vuelidate/lib/validators'

import UsernameField from './username-field.vue'
import PasswordField from './password-field.vue'

@Component({
  name: 'AuthenticationCard',
  components: {
    UsernameField,
    PasswordField
  },
  validations: {
    password: {
      required,
      minLength: minLength(6)
    },
    username: {
      required
    }
  }
})
export default class extends Vue {
  username = ''
  password = ''

  @Prop() isSignUp!: Boolean
  @Prop() buttonText!: string
  @Prop() switchText!: string
  @Prop() switchLink!: string
  @Prop() onSubmit!: Function

  get value() {
    return this.isSignUp
  }

  set value(val) {
    this.$emit('update:isSignUp', val)
  }

  submit() {
    this.$v.$touch()
    if (!this.$v.$invalid) {
      this.onSubmit(this.username, this.password)
      this.clear()
    }
  }
  clear() {
    this.$v.$reset()
    this.username = ''
    this.password = ''
  }
}
</script>

<style scoped>
.registration_error {
  margin-top: 20px;
}
.registration_error_message {
  margin-top: 20px;
  text-align: center;
  color: #ff5252;
  margin-bottom: 0;
}
.card {
  border-radius: 5px;
}
.card_switch {
  padding: 25px 15px;
  text-align: center;
}
a {
  text-decoration: none;
}
</style>