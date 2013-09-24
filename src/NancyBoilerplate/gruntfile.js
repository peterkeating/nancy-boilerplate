/**
 * Unfamiliar with Grunt? Here are some links that will allow you to learn more.
 *
 * - http://gruntjs.com
 * - http://gruntjs.com/getting-started
 * - http://howtonode.org/introduction-to-npm
 */

'use strict';

module.exports = function(grunt) {

    /**
     * Configuring the tasks available for the build process.
     */
    grunt.initConfig({

        /**
         * Analyses JavaScript files using JSHint for errors or potential problems.
         * You can customise the parameters by modifying the .jshintrc file.
         * - http://jshint.com/
         */
        jshint: {
            all: [
                'gruntfile.js',
                'assets/js/**/*.js',
                '!assets/js/vendor/**/*.js'
            ],
            options: {
                jshintrc: '.jshintrc'
            }
        },

        /**
         * Compiles Sass into a CSS file.
         */
        sass: {
            dev: {
                files: {
                    'assets/css/styles.css': 'assets/css/styles.scss'
                }
            },

            /**
             * Creates a compressed version of the project style sheet.
             */
            dist: {
                options: {
                    style: 'compressed'
                },
                files: {
                    'assets/css/styles.css': 'assets/css/styles.scss'
                }
            }
        }
    });

    /**
     * Loads libraries to assist with the build process.
     */
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-jshint');

    /**
     * Organises the front end assets ready for a production environment.
     */
    grunt.registerTask('dist', ['jshint', 'sass:dist']);
};
